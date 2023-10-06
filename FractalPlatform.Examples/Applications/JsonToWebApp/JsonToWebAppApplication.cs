using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.JsonToWebApp
{
    public class JsonToWebAppApplication : BaseApplication
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
                      var json = result.Collection
                                       .GetFirstDoc()
                                       .Value("{'JSON':$}");

                      json.ToCollection(Constants.FIRST_DOC_ID)
                          .SetDimension(DimensionType.Theme, "{'DefaultTheme':'LightBlue'}")
                          .OpenForm(result => OnStart());
                  });
        }
    }
}
