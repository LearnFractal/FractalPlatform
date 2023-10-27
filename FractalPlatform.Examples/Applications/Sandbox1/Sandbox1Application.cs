using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
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
            Client.ModifyFirstDocOf("Products")
                  .OpenForm(result => {
                        var price = result.Collection.GetFirstDoc().Select<Product>("{'Products':[!$]}").Sum(x => x.Quantity * x.Price);
                        MessageBox($"Рахунок: {price} грн.");
            });
        }
    }
}
