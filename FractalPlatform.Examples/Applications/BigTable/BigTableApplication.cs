using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;

namespace FractalPlatform.Examples.Applications.BigTable
{
    public class BigTableApplication : BaseApplication
    {
        public override void OnStart()
        {
            DocsOf("Orders").OpenForm(result => 
            {
                CreateNewDocFor("NewOrder", "Orders").OpenForm();
            });
        }
    }
}