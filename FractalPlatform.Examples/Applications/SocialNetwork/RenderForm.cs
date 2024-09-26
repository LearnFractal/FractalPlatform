using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls.Component;

namespace FractalPlatform.Examples.Applications.SocialNetwork
{
    public class RenderForm : ExtendedRenderForm
    {
        public RenderForm(BaseApplication application, DOMForm form) : base(application, form)
        {
        }

        private class PostInfo
        {
            public uint Number { get; set; }

            public string Avatar { get; set; }

            public string Who { get; set; }

            public string OnDate { get; set; }

            public string Message { get; set; }

            public string Photo { get; set; }

            public List<string> Likes { get; set; }
        }

        private class PostsInfo
        {
            public List<PostInfo> Root { get; set; }
        }

        public override string RenderComponent(ComponentDOMControl domControl)
        {
            if (domControl.ControlType == "ViewPosts")
            {
                var posts = domControl.Storage
                                     .GetAll(Application.Context)
                                     .SelectOne<PostsInfo>();

                var sb = new StringBuilder();

                sb.Append("<table border=1>");

                for (uint i = 0; i < posts.Root.Count; i++)
                {
                    posts.Root[(int)i].Number = i;
                }

                //test change

                foreach (var post in posts.Root.OrderByDescending(x => DateTime.Parse(x.OnDate, CultureInfo.InvariantCulture)))
                {
                    var html = @"<tr style='cursor:pointer' onclick=""@OnClickScript"">
                                    <td>
                                        <img style='max-width:50px;max-height:50px' src='@Avatar'>
                                    </td>
                                    <td align='left' style='width:100%' nowrap>
                                       <div>@Message</div>
                                    </td>
                                    <td nowrap>
                                       <div>@Who</div>
                                    </td>
                                    <td nowrap>
                                       <div>@OnDate</div>
                                    </td>
                                    <td nowrap>
                                       Likes: @Likes 
                                    </td>
                                 </tr>
                                 <tr>
                                    <td colspan=5>
                                       <img style='max-width:560px;max-height:560px' src='@Photo'>
                                    </td>
                                 </tr>";

                    html = html.Replace("@OnClickScript", this.OnEditRowScript(domControl.Name, post.Number));
                    html = html.Replace("@Number", post.Number.ToString());
                    html = html.Replace("@Message", post.Message);
                    html = html.Replace("@Who", post.Who);
                    html = html.Replace("@OnDate", post.OnDate);
                    html = html.Replace("@Photo", GetFileUrl(post.Photo));
                    html = html.Replace("@Avatar", GetFileUrl(post.Avatar));
                    html = html.Replace("@Likes", (post.Likes.Count - 1).ToString());

                    sb.AppendLine(html);
                }

                sb.Append("</table>");

                return sb.ToString();
            }
            else
            {
                return base.RenderComponent(domControl);
            }
        }
    }
}
