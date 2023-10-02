using System;
using System.Collections.Generic;

namespace FractalPlatform.Examples.Applications.SiteScanner
{
    public class SiteInfo
    {
        public string URL { get; set; }

        public List<string> Phrases { get; set; }

        public DateTime LastUpdate { get; set; }
    }

    public class TagInfo
    {
        public string Tag { get; set; }

        public List<SiteInfo> Sites { get; set; } = new List<SiteInfo>();
    }

    public class UserInfo
    {
        public string Name { get; set; }

        public DateTime LastScanTime { get; set; }

        public long TelegramUserID { get; set; }

        public List<TagInfo> Tags { get; set; } = new List<TagInfo>();
    }
}
