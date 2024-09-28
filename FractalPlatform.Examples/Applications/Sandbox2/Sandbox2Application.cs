using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Clients;
using FractalPlatform.Database.Engine;
using System.IO;

namespace FractalPlatform.Examples.Applications.Sandbox2
{
    public class Sandbox2Application : BaseApplication
    {
        public override void OnStart() 
        {
            var bytes = File.ReadAllBytes(@"C:\Users\tuzvy\OneDrive\Pictures\Denmark.jpg");

            var text = AI.Generate("What you see on this image ?", AIModel.GPT4o, AIImage.FromBytes(bytes));
        
        }
    }
}
