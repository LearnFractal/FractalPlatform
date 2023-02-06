using System.Collections.Generic;

namespace FractalPlatform.Applications.Portal
{
    public class CalendarMeetingInfo
    {
        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Day { get; set; }

        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public List<string> Participants { get; set; }
    }
}
