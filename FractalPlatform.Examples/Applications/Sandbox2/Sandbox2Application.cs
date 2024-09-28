using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Clients;
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
			var bytes = File.ReadAllBytes(@"C:\Users\tuzvy\OneDrive\Pictures\Me5.jpg");

            var list = new List<bool>();

            for (int i = 0; i < 100; i++)
            {
                var text = AI.Generate("I strongly needed identify age by the photo. Just say range in format: 20-25. Range should be no longer than 5 years.",
                                        AIModel.GPT4o,
                                        AIImage.FromBytes(bytes));

                list.Add(text.Text.ToLower().Contains("yes"));
            }

            var count = list.Count(x => x);
        
        }
    }
}
