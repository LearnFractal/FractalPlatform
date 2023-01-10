using System;
using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Database.Engine;

namespace FractalPlatform.Examples.Applications.PhotoAlbum
{
    public class PhotoAlbumApplication : BaseApplication
    {
        public PhotoAlbumApplication(Guid sessionId,
                                     BigDocInstance instance,
                                     IFormFactory formFactory) : base(sessionId,
                                                                     instance,
                                                                     formFactory,
                                                                     "PhotoAlbum")
        {
        }

        public override void OnStart(Context context)
        {
            Client.SetDefaultCollection("NewPhoto")
                  .WantCreateNewDocumentForArray("Photos", Constants.FIRST_DOC_ID, "{'Photos':[$]}")
                  .OpenForm(result =>
                  {
                      Client.SetDefaultCollection("Photos")
                            .GetFirstDoc()
                            .OpenForm();
                  });
        }

        public override BaseRenderForm CreateRenderForm(string formName) => new RenderForm(this);
    }
}
