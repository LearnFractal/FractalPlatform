using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.ToDoIntro
{

    public class ToDoIntroApplication : BaseApplication
    {
        private Collection _collection;

        public uint GetAmountTasks(bool isCompleted) =>
            _collection.GetWhere("{'Categories':[{'Tasks':[{'Completed':@IsCompleted}]}]}", isCompleted)
                              .Count("{'Categories':[{'Tasks':[{'Completed':$}]}]}");

        public override bool OnEventDimension(EventInfo info)
        {
            if (info.Action == "ReportButton")
            {
                var report = new
                {
                    Title = "Task progress",
                    Completed = GetAmountTasks(true),
                    NotCompleted = GetAmountTasks(false)
                };

                new Collection("Report", report.ToJson())
                    .SetUIDimension("{'Enabled':false,'Title':{'ControlType':'Label'}}")
                    .OpenForm();
            }
            else if (info.Action == "ToDoList")
            {
                //we need local collection.
                //users shouldn't share database between each other
                _collection = FirstDocOf(info.Action).ToCollection();

                _collection.OpenForm();
            }
            else
            {
                FirstDocOf(info.Action).OpenForm();
            }

            return base.OnEventDimension(info);
        }

        public override void OnStart() =>
            CreateNewDocFor("NewIntro", "Intro")
                .OpenForm(null, "Intro course", result => MessageBox("Thank you, Have a nice day !"));
    }
}