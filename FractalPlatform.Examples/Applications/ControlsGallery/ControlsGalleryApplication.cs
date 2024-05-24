using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine.Info;

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

                var ui = formInfo.Collection
                                 .GetWhere(formInfo.AttrPath)
                                 .Value(DQL("{@PathName:[{'Dimensions':{'UI':$}}]}", pathName));

                formInfo.Collection
                        .GetWhere(formInfo.AttrPath)
                        .ToCollection(DQL("{@PathName:[$]}", pathName))
                        .ResetDimension(DimensionType.UI)
                        .SetDimension(DimensionType.Enum, "{'Controls':[{'Example':{'Control':{'Items':['One','Two','Three']}}}]}")
                        .SetUIDimension(DQL("{'Style':'Save:false',@PathName:[{'Example':{'Control':@UIDimension}}]}", pathName, ui))
                        .OpenForm();

                return false;
            }
            else
            {
                return true;
            }
        }

        public override void OnStart()
        {
            FirstDocOf("ControlsAndComponents").OpenForm();
        }

        public override BaseRenderForm CreateRenderForm(DOMForm form)
        {
            return new ExtendedRenderForm(this, form);
        }
    }
}