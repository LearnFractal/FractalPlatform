using System;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.Taxi
{
    public class TaxiApplication : DashboardApplication
    {
        public void MyUser()
        {
            Client.SetDefaultCollection("Users")
                  .GetWhere("{'Name':@UserName}")
                  .OpenForm();
        }

        public void MyDashboard()
        {
            Client.SetDefaultCollection("Users")
                  .GetWhere("{'Name':@UserName}")
                  .ExtendUIDimension(@"{'Style':'Save:false','ReadOnly':true,'Password':{'Visible':false},'NewOrders':{'Visible':true}}")
                  .OpenForm();
        }

        public void NewOrder()
        {
            Client.SetDefaultCollection("NewOrder")
                  .WantCreateNewDocumentForArray("Orders", "{'Orders':[$]}")
                  .OpenForm();
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "MyUser":
                    MyUser();
                    return true;
                case "MyDashboard":
                    MyDashboard();
                    return true;
                case "NewOrder":
                    NewOrder();
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override bool OnMenuDimension(MenuInfo menuInfo)
        {
            switch (menuInfo.Action)
            {
                case "Take":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetWhere(menuInfo.AttrPath)
                              .Update("{'NewOrders':[{'Who':@UserName}]}");

                        break;
                    }
                case "Complete":
                    {
                        Client.SetDefaultCollection("Users")
                              .GetWhere(menuInfo.AttrPath)
                              .Update("{'ActiveOrders':[{'IsCompleted':true}]}");

                        break;
                    }
                default:
                    throw new InvalidOperationException();
            }

            return true;
        }

        public override void OnStart()
        {
            Login("Bob", "Bob");
        }

        public override void OnLogin(FormResult result)
        {
            Client.SetDefaultCollection("Dashboard")
                  .OpenForm();
        }
    }
}
