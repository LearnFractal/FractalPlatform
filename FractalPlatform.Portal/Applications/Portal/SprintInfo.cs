using System;
using System.Collections.Generic;

namespace FractalPlatform.Applications.Portal
{
    public class RemainedHistoryInfo
    {
        public string OnDate { get; set; }

        public uint RemainedTasks { get; set; }
    }

    public class SprintInfo
    {
        public string SprintNumber { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int RemainedTasks { get; set; }

        public List<RemainedHistoryInfo> RemainedHistory { get; set; }
    }
}
