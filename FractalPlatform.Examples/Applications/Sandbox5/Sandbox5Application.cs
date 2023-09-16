using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.Sandbox5
{
    public class Sandbox5Application : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("ToDoList")
                  .GetFirstDoc()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }
    }
}
