using System;
using BigDoc.Client.App;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.Login
{
    public class LoginApplication : DashboardApplication
    {
        public LoginApplication(Guid sessionId,
                                 BigDocInstance instance,
                                 IFormFactory formFactory) : base(sessionId,
                                                                 instance,
                                                                 formFactory,
                                                                 "Login")
        {
        }

        public override void OnStart(Context context)
        {
            Login();
        }

        protected override void OnLogin(FormResult result)
        {
            MessageBox($"You logged in as: {Context.User.Name} with role {Context.User.Roles[0]}");
        }
    }
}
