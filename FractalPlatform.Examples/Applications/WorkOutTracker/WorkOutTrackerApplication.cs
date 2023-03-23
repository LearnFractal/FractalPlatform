using BigDoc.Client.App;
using BigDoc.Client.App.Chart;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;
using System.Linq;

namespace FractalPlatform.Examples.Applications.WorkOutTracker
{
    public class WorkOutTrackerApplication : BaseApplication
    {
        public override bool OnEventDimension(EventInfo eventInfo)
        {
            if (eventInfo.Action == "Report")
            {
                var exercises = eventInfo.Collection
                                         .GetAll()
                                         .Values("{'Results':[{'Exercise':$}]}")
                                         .Distinct();

                //create chart
                var chart = new LineGraphsChartInfo
                {
                    Title = new TitleChartInfo
                    {
                        Name = "Exercises",
                        X = "Time",
                        Y = "Value"
                    },
                    Lines = exercises.Select(e =>
                    {
                        uint x = 0;

                        var line = new LineChartInfo
                        {
                            Name = e,
                            Points = eventInfo.Collection
                                              .GetWhere("{'Results':[{'Exercise':@Exercise}]}", e)
                                              .IntValues("{'Results':[{'Sum':$}]}")
                                              .Select(y => new PointChartInfo { X = x++, Y = y })
                                              .ToList()
                        };

                        return line;
                    }).ToList()
                };

                //open report
                DQL("{'Chart':@Chart}", chart)
                    .ToCollection()
                    .SetUIDimension("{'ReadOnly':true,'Chart':{'ControlType':'Chart'}}")
                    .OpenForm();
            }
            else //if (eventInfo.Action == "Configure")
            {
                Client.SetDefaultCollection("WorkOut")
                      .SetDefaultDimension(DimensionType.Enum)
                      .WantModifyExistingDocument()
                      .OpenForm();
            }

            return false;
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("WorkOut")
                  .GetAll()
                  .WantModifyExistingDocuments()
                  .OpenForm();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}
