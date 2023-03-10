using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.BTCRate
{
    public class BTCRateApplication : BaseApplication
    {
        public override void OnStart()
        {
            var query = REST.Get("https://api.coindesk.com/v1/bpi/currentprice.json")
                            .ToCollection()
                            .GetFirstDoc();

            new
            {
                USD = query.Value("{'bpi':{'USD':{'rate':$}}}"),
                EUR = query.Value("{'bpi':{'EUR':{'rate':$}}}")
            }
            .ToCollection(Constants.FIRST_DOC_ID)
            .SetUIDimension("{'ReadOnly':true,'Style':'Cancel:Refresh'}")
            .OpenForm();
        }
    }
}
