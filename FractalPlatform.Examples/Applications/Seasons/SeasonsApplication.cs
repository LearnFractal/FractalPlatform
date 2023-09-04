using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.Seasons
{
    public class SeasonsApplication : BaseApplication
    {
        public override void OnStart()
        {
            var obj = new
            {
                Title = "Watch all seasons",
                Seasons = Directory.GetDirectories(@"d:\Seasons")
                                   .Select(x => new 
                                   {
                                      Series = Directory.GetFileName(x),
                                      Episodes = Directory.GetFiles(x, "*.avi")
                                                          .Select(x => new
                                                          {
                                                            Title = Directory.GetFileName(x),
                                                            Size = $"{Directory.GetFileInfo(x).Length / 1024 / 1024} mb",
                                                            Episode = Directory.GetFileName(x)
                                                          })
                                   })
                
                
            };

            Client.SetDefaultCollection("Series")
                  .GetFirstDoc()
                  .ExtendDocument(obj.ToJson())
                  .OpenForm();
            
        }
    }
}