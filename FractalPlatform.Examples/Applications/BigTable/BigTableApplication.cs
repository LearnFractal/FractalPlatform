using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.BigTable
{
    public class BigTableApplication : BaseApplication
    {
        public override bool OnMenuDimension(MenuInfo menuInfo)
        {
            CreateNewDocFor("NewOrder", "Orders").OpenForm();

            return true;
        }

        public override void OnStart()
        {
            ModifyDocsOf("Orders").OpenForm();
        }
    }
}