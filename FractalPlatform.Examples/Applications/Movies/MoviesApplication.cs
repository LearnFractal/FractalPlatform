using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.Movies
{
    public class MoviesApplication : BaseApplication
    {
        private object _obj = new
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
                                                               Episode = @$"{Directory.GetDirectoryInfo(d).Name}\{Directory.GetFileName(f)}",
                                                               NextEpisode = "Next episode"
                                                           })
                                   })
        };

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            var idx = eventInfo.AttrPath.Key.GetLastIndexLevel2();

            eventInfo.AttrPath.Key.SetLastIndexLevel2(idx++);

            FirstDocOf("Series")
                   .ExtendDocument(_obj.ToJson())
                   .OpenForm(eventInfo.AttrPath.Key.ToDQL2());

            return true;
        }

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
                        FirstDocOf("Series")
                              .ExtendDocument(_obj.ToJson())
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