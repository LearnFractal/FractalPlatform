using FractalPlatform.Client.App;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.LoginOrRegister
{
    public class LoginOrRegisterApplication : DashboardApplication
    {
        public override void OnRegister(FormResult result) => MessageBox($"You registered as: {User.Name}");
        
        public override void OnLogin(FormResult result) => MessageBox($"You logged in as: {User.Name} with role {User.Roles[0]}");
    }
}
