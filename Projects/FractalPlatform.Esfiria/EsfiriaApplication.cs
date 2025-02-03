using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Engine.Query;

namespace FractalPlatform.CoffeePoints
{
    public class EsfiriaApplication : BaseApplication
    {
        private void Dashboard()
        {
            CloseIfOpenedForm("Dashboard");

            FirstDocOf("Dashboard").OpenForm();
        }

        public override void OnStart() => Dashboard();

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}
