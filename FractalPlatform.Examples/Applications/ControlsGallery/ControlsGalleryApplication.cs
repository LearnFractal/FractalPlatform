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
								 .Value(DQL("{@PathName:[{'Dimensions':{'UI':{'Text':$}}}]}", pathName));

                var coll = info.Collection
						.GetWhere(info.AttrPath)
						.ToCollection(DQL("{@PathName:[$]}", pathName));

				coll.DocumentStorage
					.RemoveParentAttrs2(Context, 2);

				coll.ResetDimension(DimensionType.UI)
					.SetDimension(DimensionType.Enum, "{'Example':{'Control':{'Items':['One','Two','Three']}}}")
					.SetUIDimension(@$"{{'Style':'Save:false',
                                           'ReadOnly':true,
                                           'Dimensions': {{
                                              'DocumentLabel': {{'ControlType': 'Label'}},
                                              'Document': {{'ControlType':'Code'}},
                                              'EnumLabel': {{'ControlType': 'Label'}},
                                              'Enum': {{'ControlType':'Code'}},
                                              'UILabel': {{'ControlType': 'Label'}},
                                              'UI': {{'ControlType':'Code'}}
                                           }},
                                           'Example': {{
                                              'ReadOnly':false,
                                              'Style':'Save:true',
                                              {ui.Substring(1, ui.Length - 2)}
                                           }}
                                        }}")
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