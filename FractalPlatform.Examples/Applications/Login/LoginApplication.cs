using BigDoc.Client.App;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.Login
{
    public class LoginApplication : DashboardApplication
    {
        public override void OnStart()
        {
            Login();
        }

        protected override void OnLogin(FormResult result)
        {
            MessageBox($"You logged in as: {User.Name} with role {User.Roles[0]}");
        }
    }
}
