using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.TableToJson
{
    public class TableToJsonApplication : BaseApplication
    {
        public override void OnStart()
        {
            FirstDocOf("Dashboard")
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
