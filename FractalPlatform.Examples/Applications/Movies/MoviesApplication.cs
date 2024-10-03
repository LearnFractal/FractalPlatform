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

        public override bool OnEventDimension(EventInfo info)
        {
            Context.FormFactory.ActiveFormParentAttrPath.IncreaseLastIndex();

            Context.FormFactory.NeedRefreshForm();

            _lastEpisode = Context.FormFactory.ActiveFormParentAttrPath;

            if (!Context.FormFactory.ActiveCollection.GetWhere(_lastEpisode).Any())
            {
                CloseAllForms();

                MessageBox("You watched last episode", "Next episode", MessageBoxButtonType.Ok, result => OpenSeasons());
            }

            return false;
        }

        private void OpenSeasons()
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
                                                               Title = Directory.GetFileName(f).Replace(".mp4", ""),
                                                               Size = $"{Directory.GetFileInfo(f).Length / 1024 / 1024} mb",
                                                               Episode = @$"{Directory.GetDirectoryInfo(d).Name}\{Directory.GetFileName(f)}",
                                                               Download = @$"{Directory.GetDirectoryInfo(d).Name}\{Directory.GetFileName(f)}"
                                                           })
                                   })
            };

            FirstDocOf("Series")
                  .ExtendDocument(obj.ToJson())
                  .OpenForm();

            if (_lastEpisode != null) //show last episode
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
            }
        }

        public override void OnStart()
        {
            const string password = "ps";

            if (Context.HasUrlTag && Context.UrlTag == password)
            {
                OpenSeasons();

                return;
            }
            
            InputBox("Password", "Enter password", result =>
            {
                if (result.Result)
                {
                    if (result.Collection
                              .GetFirstDoc()
                              .IsEquals("{'Password':$}", password))
                    {
                        Context.UrlTag = password;

                        OpenSeasons();
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