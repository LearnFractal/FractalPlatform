using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine;
using BigDoc.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.ToDoIntro
{

    public class ToDoIntroApplication : BaseApplication
    {
        public ToDoIntroApplication(Guid sessionId,
                               BigDocInstance instance,
                               IFormFactory formFactory) : base(sessionId,
                                                               instance,
                                                               formFactory,
                                                               "ToDoIntro")
        {
        }

        private Collection _collection;

        public uint GetAmountTasks(Context context, bool isCompleted)
        {
            return _collection.GetWhere(context, "{'Categories':[{'Tasks':[{'Completed':@IsCompleted}]}]}", isCompleted)
                              .Count("{'Categories':[{'Tasks':[{'Completed':$}]}]}");
        }

        public override bool OnEventDimension(Context context,
                                              EventInfo eventInfo)
        {
            if (eventInfo.Action == "ReportButton")
            {
                var report = new
                {
                    Title = "Task progress",
                    Completed = GetAmountTasks(context, true),
                    NotCompleted = GetAmountTasks(context, false)
                };

                new Collection("Report", ToJson(report))
                    .SetDimension(context, DimensionType.UI, "{'Enabled':false,'Title':{'ControlType':'Label'}}")
                    .OpenForm(context);
            }
            else if(eventInfo.Action == "ToDoList")
            {
                //we need local collection.
                //users shouldn't share database between each other
                _collection = Client.SetDefaultCollection(eventInfo.Action)
                                    .GetFirstDoc()
                                    .ToCollection();

                FormBuilder.OpenForm(context, _collection, Constants.FIRST_DOC_ID);
            }
            else
            {
                Client.SetDefaultCollection(eventInfo.Action)
                      .GetFirstDoc()
                      .OpenForm();
            }

            return base.OnEventDimension(context, eventInfo);
        }

        public override void OnStart(Context context)
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