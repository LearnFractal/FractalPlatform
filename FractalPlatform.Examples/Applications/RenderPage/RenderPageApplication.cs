using BigDoc.Client.App;
using BigDoc.Client.UI;

namespace FractalPlatform.Examples.Applications.HelloWorld
{
    public class RenderPageApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .OpenForm();
        }
    }
}
