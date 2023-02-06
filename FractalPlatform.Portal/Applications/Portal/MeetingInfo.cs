using System.Collections.Generic;

namespace FractalPlatform.Applications.Portal
{
    public class MeetingInfo
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public List<string> ExceptDates { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public PeriodInfo Period { get; set; }

        public List<string> Projects { get; set; }

        public List<string> Teams { get; set; }

        public List<string> Participants { get; set; }
    }
}
