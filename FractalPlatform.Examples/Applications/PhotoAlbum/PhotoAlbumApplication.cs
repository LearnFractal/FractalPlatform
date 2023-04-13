using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.PhotoAlbum
{
    public class PhotoAlbumApplication : BaseApplication
    {
        public override void OnStart()
        {
            Client.SetDefaultCollection("NewPhoto")
                  .WantCreateNewDocumentForArray("Photos", "{'Photos':[$]}")
                  .OpenForm(result =>
                  {
                      Client.SetDefaultCollection("Photos")
                            .GetFirstDoc()
                            .OpenForm();
                  });
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form) => new RenderForm(this, form);
    }
}
