/*
# Copyright(C) 2010-2023 Viacheslav Makoveichuk (email: learn.fractal@gmail.com, skype: vyacheslavm81)
# This file is part of Fractal Platform.
#
# Fractal Platform is free software : you can redistribute it and / or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# Fractal Platform is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
# GNU General Public License for more details.
#
# You should have received a copy of the GNU General Public License
# along with Foobar.  If not, see <http://www.gnu.org/licenses/>.
*/

using BigDoc.Client.App;
using BigDoc.Client.UI;
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

        public override BaseRenderForm CreateRenderForm(string formName)
        {
            return new ExtendedRenderForm(this);
        }
    }
}