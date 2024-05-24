using System.Collections.Generic;

namespace FractalPlatform.Examples.Applications.DatingGame
{
    public class Participant
    {
        public string Name { get; set; }
        
        public GenderType Gender { get; set; }

        public List<string> Questions { get; set; }

        public string Choose { get; set; }

        public string Photo { get; set; }

        public string Contacts { get; set; }

        public string GenderGroup => Gender == GenderType.Boy ? "Boys" : "Girls";
    }
}
