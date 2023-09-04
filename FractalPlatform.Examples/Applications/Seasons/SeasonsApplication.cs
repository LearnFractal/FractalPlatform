using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.VideoLibrary
{
    public class SeasonsApplication : BaseApplication
    {
        public override void OnStart()
        {
            var obj = new
            {
                Title = "Seasons",
                Seasons = Directory.GetDirectories(@"d:\Seasons")
                                   .Select(x => new 
                                   {
                                      Series = x,
                                      Episodes = Directory.GetFiles(x, "*.mp4")
                                                          .Select(x => new
                                                          {
                                                            Title = Directory.GetFileName(x),
                                                            Size = $"{Directory.GetFileInfo(x).Length / 1024 / 1024} mb",
                                                            Video = Directory.GetFileName(x)
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