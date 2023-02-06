namespace FractalPlatform.Applications.Portal
{
    public class PeriodInfo
    {
        public bool Once { get; set; }

        public bool EveryDay { get; set; }

        public bool EveryWorkingDay { get; set; }

        public string EveryWeekAt { get; set; }

        public int EveryPeriodInDays { get; set; }
    }

}
