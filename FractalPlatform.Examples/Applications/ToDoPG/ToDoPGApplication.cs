using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.ToDoPG
{
    public class ToDoPGApplication : BaseApplication
    {
        public override void OnStart() => ModifyFirstDocOf("ToDoList").OpenForm();
    }
}
