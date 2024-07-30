using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.ProjectList
{
    public class ProjectListApplication : BaseApplication
    {
        public override void OnStart()
        {
            ModifyFirstDocOf("Projects").OpenForm();
        }
    }
}