using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.ListOfProjects
{
    public class ListOfProjectsApplication : BaseApplication
    {
        public override void OnStart() => ModifyFirstDocOf("Projects").OpenForm();
    }
}