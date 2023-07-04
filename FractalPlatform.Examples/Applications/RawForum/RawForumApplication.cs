using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.RawForum
{
    public class RawForumApplication : DashboardApplication
    {
        private void Dashboard()
        {
            var topics = Client.SetDefaultCollection("Topics")
                               .GetAll()
                               .ToStorage();

            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .ToCollection()
                  .MergeToArrayPath(topics, new AttrPath("Topics"))
                  .OpenForm();
        }

        public override void OnStart()
        {
            Dashboard();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}
