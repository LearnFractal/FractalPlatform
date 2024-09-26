using System;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.Taxi
{
    public class TaxiApplication : DashboardApplication
    {
        public bool MyUser() => DocsWhere("Users", "{'Name':@UserName}").OpenForm();
        
        public bool MyDashboard() => DocsWhere("Users", "{'Name':@UserName}")
                                        .ExtendUIDimension(@"{'Style':'Save:false','ReadOnly':true,'Password':{'Visible':false},'NewOrders':{'Visible':true}}")
                                        .OpenForm();

        public bool NewOrder() => CreateNewDocForArray("NewOrder", "Orders", "{'Orders':[$]}")
                                    .OpenForm();

        public override bool OnEventDimension(EventInfo info) =>
            info.Action switch
            {
                "MyUser" => MyUser(),
                "MyDashboard" => MyDashboard(),
                "NewOrder" => NewOrder(),
                _ => base.OnEventDimension(info)
            };

        public override bool OnMenuDimension(MenuInfo info) =>
            info.Action switch
            {
                "Take" => DocsWhere("Users", info.AttrPath).Update("{'NewOrders':[{'Who':@UserName}]}"),
                "Complete" => DocsWhere("Users", info.AttrPath).Update("{'ActiveOrders':[{'IsCompleted':true}]}"),
                _ => throw new InvalidOperationException()
            };

        public override void OnStart() => Login("Bob", "Bob");
        
        public override void OnLogin(FormResult result) => FirstDocOf("Dashboard").OpenForm();
    }
}