using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderPageApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .OpenForm();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form)
        {
            return new RenderForm(this);
        }
    }
}
