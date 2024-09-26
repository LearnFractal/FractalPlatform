using Newtonsoft.Json;
using System;

namespace FractalPlatform.CreateLayout
{
    public class Options
    {
        public string LayoutPath { get; set; }

        public string DeploymentToolPath { get; set; }

        [JsonIgnore]
        public string FilesPath 
        {
            get
            {
                if (!string.IsNullOrEmpty(LayoutPath))
                {
                    return LayoutPath.Replace("\\layouts\\", "\\files\\")
                                     .Replace(".html", "");
                }
                else
                {
                    return null;
                }
            }
        }

        [JsonIgnore]
        public string DBName
        {
            get
            {
                if (!string.IsNullOrEmpty(LayoutPath))
                {
                    var parts = LayoutPath.Split(new string[] { "\\" }, StringSplitOptions.None);

                    return parts[parts.Length - 2];
                }
                else
                {
                    return null;
                }
            }
        }

        [JsonIgnore]
        public string CollName
        {
            get
            {
                if (!string.IsNullOrEmpty(LayoutPath))
                {
                    var parts = LayoutPath.Split(new string[] { "\\" }, StringSplitOptions.None);

                    return parts[parts.Length - 1].Replace(".html", "");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
