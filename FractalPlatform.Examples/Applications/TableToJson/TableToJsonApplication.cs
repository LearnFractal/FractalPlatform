using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.TableToJson
{
    public class TableToJsonApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm(result =>
                  {
                      var table = result.Collection
                                        .GetFirstDoc()
                                        .Value("{'Table':$}");

                      /*
                                  
                      */
                  });
        }
    }
}
