using System.IO;
using System.Linq;
using BigDoc.Client.App;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.VideoLibrary
{
    public class VideoLibraryApplication : BaseApplication
    {
        public override void OnStart()
        {
            //This code just as example. The assembly cannot be deployed with security issue to the server
            //(means reference to System.IO.Directory.GetFiles)
            /*
            var obj = new
            {
                Title = "Video Library",
                Videos = Directory.GetFiles(@"c:\VideoLibrary", "*.mp4")
                                  .Select(x => new
                                  {
                                      Title = Path.GetFileName(x),
                                      Size = $"{new FileInfo(x).Length / 1024 / 1024} mb",
                                      Video = Path.GetFileName(x)
                                  })
            };

            Client.SetDefaultCollection("Videos")
                  .GetFirstDoc()
                  .ExtendDocument(obj.ToJson())
                  .OpenForm();
            */
        }
    }
}