using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.ToDo
{
    public class ToDoApplication : BaseApplication
    {
        public override void OnStart() => ModifyFirstDocOf("ToDoList").OpenForm();
    }
}
