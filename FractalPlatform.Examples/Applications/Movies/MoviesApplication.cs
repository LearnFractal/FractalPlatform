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
        private static AttrPath _lastEpisode;

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            Context.FormFactory.ActiveFormParentAttrPath.IncreaseLastIndex();

            Context.FormFactory.NeedRefreshForm();

            _lastEpisode = Context.FormFactory.ActiveFormParentAttrPath;

            return false;
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
                                                               NextEpisode = "Next episode",
                                                               Title = Directory.GetFileName(f),
                                                               Size = $"{Directory.GetFileInfo(f).Length / 1024 / 1024} mb",
                                                               Episode = @$"{Directory.GetDirectoryInfo(d).Name}\{Directory.GetFileName(f)}",
                                                               Download = @$"{Directory.GetDirectoryInfo(d).Name}\{Directory.GetFileName(f)}"
                                                           })
                                   })
                        };

                        FirstDocOf("Series")
                              .ExtendDocument(obj.ToJson())
                              .OpenForm();

                        //show last episode
                        if (_lastEpisode != null)
                        {
                            FirstDocOf("Series")
                              .ExtendDocument(obj.ToJson())
                              .OpenForm(result =>
                              {
                                  if (!result.Result)
                                  {
                                      _lastEpisode = null;
                                  }
                              });

                            Context.FormFactory.ActiveFormParentAttrPath = _lastEpisode;
                        }
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