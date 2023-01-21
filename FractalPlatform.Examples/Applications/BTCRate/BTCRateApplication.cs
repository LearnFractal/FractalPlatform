using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.BTCRate
{
    public class BTCRateApplication : BaseApplication
    {
        public BTCRateApplication(Guid sessionId,
                                  BigDocInstance instance,
                                  IFormFactory formFactory) : base(sessionId,
                                                                   instance,
                                                                   formFactory)
        {
        }

        public override void OnStart(Context context)
        {
            var query = REST.Get("https://api.coindesk.com/v1/bpi/currentprice.json")
                            .ToCollection(context)
                            .GetFirstDoc(context);

            new
            {
                USD = query.Value("{'bpi':{'USD':{'rate':$}}}"),
                EUR = query.Value("{'bpi':{'EUR':{'rate':$}}}")
            }
            .ToCollection(context, Constants.FIRST_DOC_ID)
            .SetDimension(context, DimensionType.UI, "{'ReadOnly':true,'Style':'Cancel:Refresh'}")
            .OpenForm(context);
        }
    }
}
