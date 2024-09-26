using System;

namespace FractalPlatform.UTube
{
    public class VideoInfo
    {
        public string UID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Video { get; set; }

        public string Preview { get; set; }

        public DateTime OnDate { get; set; }

        public string OnDateLabel { get; set; }

        public uint CountViews { get; set; }

        public uint CountLikes { get; set; }

        public uint CountComments { get; set; }
    }
}
