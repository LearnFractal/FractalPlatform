using FractalPlatform.Client.App;
using FractalPlatform.Client.UI;
using FractalPlatform.Common.Enums;
using FractalPlatform.Database.Engine;

namespace FractalPlatform.Examples.Applications.ImageHosting
{
    public class ImageHostingApplication : BaseApplication
    {
        public override void OnStart()
        {
            if (Context.HasUrlTag)
            {
                new
                {
                    Image = Context.UrlTag
                }
                .ToCollection(string.Empty)
                .SetUIDimension("{'Style':'Save:false;Cancel:false','Image':{'ControlType':'Picture'}}")
                .OpenForm();
            }
            else
            {
                new
                {
                    Title = "Upload your image",
                    Image = "",
                    Captcha = ""
                }
                .ToCollection(string.Empty)
                .SetUIDimension("{'Style':'Save:Upload;Cancel:false','Title':{'ControlType':'Label'},'Image':{'ControlType':'UploadFile'},'Captcha':{'Style':'Type:Captcha'}}")
                .SetDimension(DimensionType.Validation, "{'Image':{'IsRequired':true}}")
                .OpenForm(result =>
                {
                    Context.UrlTag = result.FindFirstValue("Image");

                    new
                    {
                        Title = "Your image has been uploaded",
                        URL = $"https://fraplat.com/jupiter/ImageHosting/?tag={Context.UrlTag}",
                        Image = Context.UrlTag
                    }
                    .ToCollection(string.Empty)
                    .SetUIDimension("{'Style':'Save:false;Cancel:View','Title':{'ControlType':'Label'},'URL':{'ControlType':'TextBox','ReadOnly':true},'Image':{'ControlType':'Picture'}}")
                    .OpenForm();
                });
            }
        }
    }
}
