using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.Sandbox1
{
    public class Sandbox1Application : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm(result =>
                  {
                      var json = result.Collection
                                       .GetFirstDoc()
                                       .Value("{'JSON':$}");

                      json.ToCollection(Constants.FIRST_DOC_ID)
                          .OpenForm(result => OnStart());
                  });
        }
    }
}
