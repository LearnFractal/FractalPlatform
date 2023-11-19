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

        public void ViewStock()
        {
            DocsOf("Stock").OpenForm();
        }

        public void ViewOrders()
        {
            DocsOf("Orders").OpenForm();
        }

        public void NewOrder()
        {
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
        }

        public override bool OnMenuDimension(MenuInfo menuInfo)
        {
            switch (menuInfo.Action)
            {
                case "AddToCart":
                    {
                        var coll = menuInfo.Collection;

                        //read entities
                        var stock = coll.GetWhere(menuInfo.AttrPath).SelectOne<Stock>();

                        var cart = coll.GetDoc(menuInfo.DocID).SelectOne<Cart>();

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
                        coll.GetDoc(menuInfo.DocID)
                            .UpdateByObject(cart);

                        return false; //do not reload data
                    }
                default:
                    throw new NotImplementedException();
            }
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Stock":
                    ViewStock();
                    return true;
                case "NewOrder":
                    NewOrder();
                    return true;
                case "Orders":
                    ViewOrders();
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override void OnStart()
        {
            Login("Bob", "Bob");
        }

        public override void OnLogin(FormResult result)
        {
            FirstDocOf("Dashboard").OpenForm();
        }
    }
}
