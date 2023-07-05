using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;

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

        private void Users()
        {
            Client.SetDefaultCollection("Users")
                  .GetAll()
                  .OpenForm();
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "Users":
                    Users();
                    return true;
                case "LoginButton":
                    var login = eventInfo.Collection
                                         .GetFirstDoc()
                                         .Value("{'Login':$}");

                    var password = eventInfo.Collection
                                            .GetFirstDoc()
                                            .Value("{'Password':$}");

                    Login(login, password);
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override void OnStart()
        {
            Dashboard();
        }
    }
}
