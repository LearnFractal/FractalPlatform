using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine.Info;

namespace FractalPlatform.Examples.Applications.ControlsGallery
{
    public class ControlsGalleryApplication : BaseApplication
    {
        public override bool OnOpenForm(FormInfo info)
        {
            if (!info.AttrPath.IsEmpty &&
                 info.AttrPath.Count == 2)
            {
                var pathName = info.AttrPath.GetFirstPath();

                var ui = info.Collection
                                 .GetWhere(info.AttrPath)
                                 .Value(DQL("{@PathName:[{'Dimensions':{'UI':$}}]}", pathName));

                info.Collection
                        .GetWhere(info.AttrPath)
                        .ToCollection(DQL("{@PathName:[$]}", pathName))
                        .ResetDimension(DimensionType.UI)
                        .SetDimension(DimensionType.Enum, "{'Controls':[{'Example':{'Control':{'Items':['One','Two','Three']}}}]}")
                        .SetUIDimension(DQL("{'Style':'Save:false','ReadOnly':true,@PathName:[{'Example':{'ReadOnly':false,'Style':'Save:true','Control':@UIDimension}}]}", pathName, ui))
                        .OpenForm();

                return false;
            }
            else
            {
                return true;
            }
        }

        public override void OnStart() => FirstDocOf("ControlsAndComponents").OpenForm();
        
        public override BaseRenderForm CreateRenderForm(DOMForm form) => new ExtendedRenderForm(this, form);
    }
}