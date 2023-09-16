using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.Sandbox3
{
    public class Sandbox3Application : BaseApplication
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
