using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.ToDoIntro
{

    public class ToDoIntroApplication : BaseApplication
    {
        private Collection _collection;

        public uint GetAmountTasks(bool isCompleted)
        {
            return _collection.GetWhere("{'Categories':[{'Tasks':[{'Completed':@IsCompleted}]}]}", isCompleted)
                              .Count("{'Categories':[{'Tasks':[{'Completed':$}]}]}");
        }

        public override bool OnEventDimension(EventInfo eventInfo)
        {
            if (eventInfo.Action == "ReportButton")
            {
                var report = new
                {
                    Title = "Task progress",
                    Completed = GetAmountTasks(true),
                    NotCompleted = GetAmountTasks(false)
                };

                new Collection("Report", report.ToJson())
                    .SetDimension(DimensionType.UI, "{'Enabled':false,'Title':{'ControlType':'Label'}}")
                    .OpenForm();
            }
            else if(eventInfo.Action == "ToDoList")
            {
                //we need local collection.
                //users shouldn't share database between each other
                _collection = Client.SetDefaultCollection(eventInfo.Action)
                                    .GetFirstDoc()
                                    .ToCollection();

                _collection.OpenForm();
            }
            else
            {
                Client.SetDefaultCollection(eventInfo.Action)
                      .GetFirstDoc()
                      .OpenForm();
            }

            return base.OnEventDimension(eventInfo);
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("NewIntro")
                  .GetFirstDoc()
                  .WantCreateNewDocumentFor("Intro")
                  .OpenForm(null, "Intro course", result =>
                  {
                      MessageBox("Thank you, Have a nice day !");
                  });
        }
    }
}