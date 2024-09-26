using System;
using System.Linq;
using System.Collections.Generic;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Client.App;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.Supermarket
{
    public class SupermarketApplication : DashboardApplication
    {
        private class ProductItem
        {
            public string Product { get; set; }
            public int Count { get; set; }
            public int Price { get; set; }
        }

        private class Stock
        {
            public List<ProductItem> StockProducts { get; set; }
        }

        private class Cart
        {
            public List<ProductItem> Products { get; set; }
        }

        public bool ViewStock() => DocsOf("Stock").OpenForm();
        
        public bool ViewOrders() => DocsOf("Orders").OpenForm();

        public bool NewOrder() =>
            CreateNewDocFor("NewOrder", "Orders")
                  .OpenForm(result => 
                  {
                      if (result.Result)
                      {
                          var cart = result.Collection
                                           .GetDoc(result.DocID)
                                           .SelectOne<Cart>();

                          Client.BeginTran(TranType.RepeatableRead);

                          foreach(var product in cart.Products)
                          {
                              FirstDocOf("Stock").AndWhere("{'Products':[{'Product':@Product}]}", product.Product)
                                                 .Update("{'Products':[{'Count':Sub(@Count)}]}", product.Count);

                          }

                          Client.CommitTran();
                      }
                  });
        
        public override bool OnMenuDimension(MenuInfo info)
        {
            switch (info.Action)
            {
                case "AddToCart":
                    {
                        var coll = info.Collection;

                        //read entities
                        var stock = coll.GetWhere(info.AttrPath).SelectOne<Stock>();

                        var cart = coll.GetDoc(info.DocID).SelectOne<Cart>();

                        var stockProduct = stock.StockProducts[0];

                        //add to cart
                        var product = cart.Products.FirstOrDefault(x => x.Product == stockProduct.Product);

                        if (product != null)
                        {
                            product.Count++;

                            product.Price = product.Count * stockProduct.Price;
                        }
                        else
                        {
                            stockProduct.Count = 1;

                            cart.Products.Add(stockProduct);
                        }

                        //save
                        coll.GetDoc(info.DocID)
                            .UpdateByObject(cart);

                        return false; //do not reload data
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public override bool OnEventDimension(EventInfo info) =>
            info.Action switch
            {
                "Stock" => ViewStock(),
                "NewOrder" => NewOrder(),
                "Orders" => ViewOrders(),
                _ => base.OnEventDimension(info)
            };
        
        public override void OnStart() => Login("Bob", "Bob");
        
        public override void OnLogin(FormResult result) => FirstDocOf("Dashboard").OpenForm();
    }
}
