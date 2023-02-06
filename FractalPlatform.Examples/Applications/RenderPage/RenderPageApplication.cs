using BigDoc.Client.App;
using BigDoc.Client.UI;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderPageApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm();
        }

        public override BaseRenderForm CreateRenderForm(string formName)
        {
            return new RenderForm(this);
        }
    }
}
