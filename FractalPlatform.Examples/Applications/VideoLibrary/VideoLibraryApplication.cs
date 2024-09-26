using System.Linq;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.VideoLibrary
{
    public class VideoLibraryApplication : BaseApplication
    {
        public override void OnStart() =>
            FirstDocOf("Videos")
                  .ExtendDocument(new
                  {
                      Title = "Video Library",
                      Videos = Directory.GetFiles(@"c:\VideoLibrary", "*.mp4")
                                  .Select(x => new
                                  {
                                      Title = Directory.GetFileName(x),
                                      Size = $"{Directory.GetFileInfo(x).Length / 1024 / 1024} mb",
                                      Video = Directory.GetFileName(x)
                                  })
                  }.ToJson())
                  .OpenForm();
    }
}