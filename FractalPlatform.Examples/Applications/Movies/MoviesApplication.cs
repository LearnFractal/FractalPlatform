using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.Movies
{
    public class MoviesApplication : BaseApplication
    {
        public override void OnStart()
        {
            InputBox("Password", "Enter password", result =>
            {
                if (result.Result)
                {
                    if (result.Collection
                              .GetFirstDoc()
                              .IsEquals("{'Password':$}", "ps"))
                    {
                        var obj = new
                        {
                            Title = "Watch all seasons",
                            Seasons = Directory.GetDirectories(@"d:\Movies")
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