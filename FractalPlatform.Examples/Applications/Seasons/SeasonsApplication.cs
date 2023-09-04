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
                                   .Select(d => new
                                   {
                                       Series = Directory.GetFileName(d),
                                       Episodes = Directory.GetFiles(d, "*.mp4")
                                                           .Select(f => new
                                                           {
                                                              Title = Directory.GetFileName(f),
                                                              Size = $"{Directory.GetFileInfo(f).Length / 1024 / 1024} mb",
                                                              Episode = @$"{Directory.GetDirectoryInfo(d).Name}\{Directory.GetFileName(f)}"
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