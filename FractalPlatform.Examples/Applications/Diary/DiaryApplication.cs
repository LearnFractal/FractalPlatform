using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.Diary
{
    public class DiaryApplication : BaseApplication
    {
        public override bool OnEventDimension(EventInfo eventInfo)
        {
            switch (eventInfo.Action)
            {
                case "NewDay":
                    CreateNewDocFor("NewDay", "Days").OpenForm();
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
            FirstDocOf("Dashboard").OpenForm();
        }
    }
}