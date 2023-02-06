namespace FractalPlatform.Applications.Portal
{
    public class StatInfo
    {
        public uint Period { get; set; }

        public uint Requests { get; set; }

        public uint Errors { get; set; }

        public uint TimeInMsec { get; set; }

        public uint AverageTimeInMsec
        {
            get
            {
                if (TimeInMsec > 0)
                {
                    if (Requests > 0)
                    {
                        return TimeInMsec / Requests;
                    }
                    else
                    {
                        return TimeInMsec;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
