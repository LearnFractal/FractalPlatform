using BigDoc.Client.App;
using BigDoc.Client.UI;
using BigDoc.Client.UI.DOM;
using BigDoc.Common.Enums;
using BigDoc.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.ControlsGallery
{
    public class ControlsGalleryApplication : BaseApplication
    {
        public override bool OnOpenForm(FormInfo formInfo)
        {
            if (!formInfo.AttrPath.IsEmpty &&
                 formInfo.AttrPath.Count == 2)
            {
                var pathName = formInfo.AttrPath.GetFirstPath();

                var uiDimension = formInfo.Collection
                                          .GetWhere(formInfo.AttrPath)
                                          .Value("{@PathName:[{'Dimensions':{'UI':$}}]}", pathName);

                formInfo.Collection
                        .ExtendDimension(DimensionType.UI,
                                         DQL("{@PathName:[{'Example':{'Control':@UIDimension}}]}", pathName, uiDimension));
            }

            return base.OnOpenForm(formInfo);
        }

        public override void OnStart()
        {
            Client.SetDefaultCollection("ControlsAndComponents")
                  .GetFirstDoc()
                  .OpenForm();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form)
        {
            return new ExtendedRenderForm(this, form);
        }
    }
}