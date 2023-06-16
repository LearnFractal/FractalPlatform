using System;

namespace FractalPlatform.CreateLayout
{
    public class Options
    {
        public string BaseUrl { get; set; }

        public string LayoutPath { get; set; }

        public string DeploymentToolPath { get; set; }

        public string FilesPath => LayoutPath.Replace("\\layouts\\", "\\files\\")
                                             .Replace(".html", "");

        public string DBName
        {
            get
            {
                var parts = LayoutPath.Split(new string[] { "\\" }, StringSplitOptions.None);

                return parts[parts.Length - 2];
            }
        }

        public string CollName
        {
            get
            {
                var parts = LayoutPath.Split(new string[] { "\\" }, StringSplitOptions.None);

                return parts[parts.Length - 1].Replace(".html", ""); ;
            }
        }
    }
}
