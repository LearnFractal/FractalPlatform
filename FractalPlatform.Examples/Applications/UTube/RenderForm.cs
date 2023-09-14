using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM.Controls.Component;
using System.Text;

namespace FractalPlatform.Examples.Applications.UTube
{
    public class RenderForm : ExtendedRenderForm
    {
        public RenderForm(BaseApplication application, DOMForm form) : base(application, form)
        {
        }

        public override string RenderComponent(ComponentDOMControl domControl)
        {
            var sb = new StringBuilder();

            if (domControl.ControlType == "Videos")
            {
                var videos = domControl.Storage
                                       .GetAll(Application.Context)
                                       .Select<VideoInfo>("{'Root':[!$]}");

                sb.Append("<td><table><tr>");

                for (uint i = 0; i < videos.Length; i++)
                {
                    var video = videos[i];

                    sb.Append("<td>")
                      .Append("<a href=\"").Append(this.OnEditRowUrl(domControl, i)).Append("\">")
                      .Append(video.Name)
                      .Append("<br><i>")
                      .Append(video.Description)
                      .Append("</i>")
                      .Append("</a>")
                      .Append("<br><video width=320 height=215 controls><source src=\"").Append(GetFileUrl(video.Video)).Append("\" type=\"video/mp4\"></video>")
                      .Append("<br>")
                      .Append("<a href=\"").Append(this.OnEditRowUrl(domControl, i)).Append("\">")
                      .Append("Views:").Append(video.CountViews)
                      .Append("&nbsp;")
                      .Append("Likes:").Append(video.Likes.Count)
                      .Append("</a>")
                      .Append("</td>");
                }

                sb.Append("</tr></table></td>");

                return sb.ToString();
            }
            else
            {
                return base.RenderComponent(domControl);
            }
        }
    }
}
