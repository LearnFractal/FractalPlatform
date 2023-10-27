using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using System.Linq;

namespace FractalPlatform.Examples.Applications.Sandbox1
{
    public class Sandbox1Application : BaseApplication
    {
        private class Product 
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
        }

        public override void OnStart()
        {
            new { Products = new[] 
                    { new Product { Name = "Хліб", Quantity = 1, Price = 1 }, 
                      new Product { Name = "Молоко", Quantity = 2, Price = 5 } } }
            .ToCollection(Constants.FIRST_DOC_ID)
            .SetUIDimension("{'Style':'Save:Calculate;Cancel:false'}")
            .OpenForm(result => {
                var price = result.Collection.GetFirstDoc().Select<Product>("{'Products':[!$]}").Sum(x => x.Quantity * x.Price);
                MessageBox($"Рахунок: {price} грн.");
            });
        }
    }
}
