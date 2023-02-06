using System.Collections.Generic;

namespace FractalPlatform.Applications.Portal
{
    public class ChatInfo
    {
        public string Name { get; set; }

        public List<ChatMessageInfo> Messages { get; set; }

        public List<string> Users { get; set; }

        public List<string> Teams { get; set; }

        public List<string> Projects { get; set; }
    }
}
