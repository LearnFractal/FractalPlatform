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
                                var points = DocsOf("Points").ToStorage();
                                var day = DocsWhere("Days", result.TargetDocID);

                                var sumPoints = 0;

                                day.ToStorage()
                                      .ScanKeysAndValues2(Context,
                                                          KeyMap.Empty,
                                                          (keyMap, attrValue, data) =>
                                                          {
                                                              if (attrValue.GetBoolValue())
                                                              {
                                                                  var currKeyMap = keyMap.Clone();
                                                                  currKeyMap.SetDocID2(Constants.FIRST_DOC_ID);

                                                                  AttrValue currPoints;

                                                                  if (points.GetValueByKey2(Context, currKeyMap, out currPoints))
                                                                  {
                                                                      sumPoints += currPoints.GetIntValue();
                                                                  };
                                                              }

                                                              return true;
                                                          },
                                                          null,
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