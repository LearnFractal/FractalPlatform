using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.RawForum
{
    public class RawForumApplication : DashboardApplication
    {
        private void Dashboard()
        {
            Client.SetDefaultCollection("Dashboard")
                  .OpenForm();
        }

        public override void OnStart()
        {
            Dashboard();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}
