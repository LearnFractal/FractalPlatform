using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderPageApplication : BaseApplication
    {
        public override void OnStart() => 
            FirstDocOf("Dashboard").OpenForm();
        
        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}
