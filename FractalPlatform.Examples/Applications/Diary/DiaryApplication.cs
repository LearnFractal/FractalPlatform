using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;
using FractalPlatform.Database.Storages;

namespace FractalPlatform.Examples.Applications.Diary
{
    public class DiaryApplication : BaseApplication
    {
        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "NewDay":
                    CreateNewDocFor("NewDay", "Days")
                        .OpenForm(result =>
                        {
                            if (result.Result)
                            {
                                var points = DocsOf("Points").ToCollection();
                                var day = DocsWhere("Days", result.TargetDocID);

                                var sumPoints = 0;

                                day.ToCollection()
                                    .ScanKeysAndValues((attrPath, attrValue) =>
                                                       {
                                                           if (attrValue.GetBoolValue())
                                                           {
                                                               var currAttrPath = attrPath.Clone();

                                                               attrPath.DocID = Constants.FIRST_DOC_ID;

                                                               var currPoints = points.GetValueByPath(currAttrPath);

                                                               if (currPoints != null)
                                                               {
                                                                   sumPoints += currPoints.GetIntValue();
                                                               }
                                                           }

                                                           return true;
                                                       },
                                                       result.TargetDocID);

                                day.Update("{'Points':@Points}", sumPoints);
                            }
                        });
                    return true;
                case "Days":
                    DocsOf("Days").OpenForm();
                    return true;
                default:
                    return base.OnEventDimension(eventInfo);
            }
        }

        public override void OnStart()
        {
            FirstDocOf("Dashboard")
                .OpenForm();
        }
    }
}