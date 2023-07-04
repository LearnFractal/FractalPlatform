using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.RawForum
{
    public class RawForumApplication : DashboardApplication
    {
        private void Dashboard()
        {
            //var topics = Client.SetDefaultCollection("Topics")
            //                   .GetAll()
            //                   .ToStorage();

            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  //.ToCollection()
                  //.MergeToArrayPath(topics, new AttrPath("Topics"))
                  .OpenForm();
        }

        public override void OnStart()
        {
            Dashboard();
        }
    }
}
