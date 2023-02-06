using System.Text;
using BigDoc.Client.App;
using System.Collections.Generic;
using BigDoc.Client.UI.DOM.Controls.Component;
using BigDoc.Common.Serialization;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderForm: ExtendedRenderForm
    {
        public RenderForm(BaseApplication application) : base(application)
        {
        }

        public class MeetingInfo
        { 
            public string Title { get; set; }

            public string StartTime { get; set; }

            public string EndTime { get; set; }
        }


        public class DayInfo
        {
            public string Date { get; set; }

            public string Day { get; set; }

            public List<MeetingInfo> Meetings { get; set; }
        }

        public class CalendarInfo
        {
            public List<DayInfo> Root { get; set; }
        }

        public override string RenderComponent(ComponentDOMControl domControl)
        {
            var model = JsonConvert.Deserialize<CalendarInfo>(domControl.Value);

            var sb = new StringBuilder();

            sb.Append(model.Root.Count);

            return sb.ToString();
        }
    }
}
