using System;
using System.Collections.Generic;

namespace FractalPlatform.Client.WebApp.Applications.UTube
{
    public class VideoInfo
    {
        public string UID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public DateTime OnDate { get; set; }

        public uint CountViews { get; set; }

        public List<string> Likes { get; set; }
    }
}
