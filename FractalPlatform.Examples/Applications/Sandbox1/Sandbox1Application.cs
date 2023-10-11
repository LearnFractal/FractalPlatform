using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.Sandbox1
{
    public class Sandbox1Application : BaseApplication
    {
        public override object OnComputedDimension(ComputedInfo computedInfo)
        {
            bool isValid = true;

            Validators.ValidateJson(computedInfo.AttrValue.ToString(), (x, y, z) => isValid = false);

            return isValid;
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("Dashboard")
                  .GetFirstDoc()
                  .OpenForm(result =>
                  {
                      var json = result.Collection.FindFirstValue("JSON");

                      json.ToCollection(Constants.FIRST_DOC_ID)
                          .OpenForm(result => OnStart());
                  });
        }
    }
}
