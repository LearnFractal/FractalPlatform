using BigDoc.Client.App;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.LoginOrRegister
{
    public class LoginOrRegisterApplication : DashboardApplication
    {
        protected override void OnRegister(FormResult result)
        {
            MessageBox($"You registered as: {User.Name}");
        }

        protected override void OnLogin(FormResult result)
        {
            MessageBox($"You logged in as: {User.Name} with role {User.Roles[0]}");
        }
    }
}
