using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.Template
{
    public class TemplateApplication : BaseApplication
    {
        public override void OnStart() => ModifyFirstDocOf("TemplateList").OpenForm();
    }
}