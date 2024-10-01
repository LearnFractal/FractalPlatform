using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Clients;
using FractalPlatform.Database.Engine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FractalPlatform.Examples.Applications.Sandbox2
{
    public class Sandbox2Application : BaseApplication
    {
        public override void OnStart() 
        {
			var bytes = File.ReadAllBytes(@"C:\Users\tuzvy\OneDrive\Pictures\Me6.jpg");

            var list = new List<bool>();

            for (int i = 0; i < 100; i++)
            {
                var text = AI.Generate("Create html with weather data. Add <control> tag to html everywhere where data can be changed, for e.g.: <html><b>span</span><span><control>10</control></span></html>",
                                        AIModel.Llama32);

                list.Add(text.Text.ToLower().Contains("yes"));
            }

            var count = list.Count(x => x);
        
        }
    }
}
