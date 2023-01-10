using System.Collections.Generic;

namespace FractalPlatform.Deployment
{
    public class Options
    {
        public string BaseUrl { get; set; }
        
        public string AppName { get; set; }
        
        public List<string> Assemblies { get; set; }

        public bool IsDeployDatabase { get; set; }

        public bool IsRecreateDatabase { get; set; }

        public bool IsDeployFiles { get; set; }
  
        public bool IsDeployApplication { get; set; }

        public string DeploymentKey { get; set; }

        public bool IsRunBrowser { get; set; }
    }
}
