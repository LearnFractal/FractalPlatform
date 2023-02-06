using System.Collections.Generic;
using System.Linq;

namespace FractalPlatform.Applications.Portal
{
    public class UserInfo
    {
        public string Name { get; set; }

        public List<string> Teams { get; set; }

        public List<string> Projects { get; set; }

        public string TeamsAsString => string.Join(',', Teams.Select(x => $"'{x}'")
                                                             .ToArray());

        public string ProjectsAsString => string.Join(',', Projects.Select(x => $"'{x}'")
                                                                   .ToArray());
    }
}
