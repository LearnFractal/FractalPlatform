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
            InputBox("Password", "Enter password", result =>
            {
                if (result.Result)
                {
                    var password = result.Collection
                                         .GetFirstDoc()
                                         .Value("{'Password':$}");

                    if (password == "ps")
                    {
                        var obj = new
                        {
                            Title = "Watch all seasons",
                            Seasons = Directory.GetDirectories(@"d:\Seasons")
                                   .Select(x => new
                                   {
                                       Series = Directory.GetFileName(x),
                                       Episodes = Directory.GetFiles(x, "*.mp4")
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
                    else
                    {
                        MessageBox("Wrong password.");
                    }
                }
            });
        }
    }
}