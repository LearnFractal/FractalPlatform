using BigDoc.Client.UI.DOM.Controls;
using System.Text;
using BigDoc.Client.App;
using BigDoc.Client.UI.DOM.Controls.Grid;
using System.Data;
using System.Collections.Generic;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderForm: ExtendedRenderForm
    {
        public RenderForm(BaseApplication application) : base(application)
        {
        }

        public class CommentInfo
        {
            public string OnDate { get; set; }
        }

        public class GridInfo
        {
            public string Title { get; set; }

            public string OnDate { get; set; }

            public string Who { get; set; }

            public string Picture { get; set; }

            public List<CommentInfo> Comments { get; set; }

            public List<string> Likes { get; set; }
        }


        public override string RenderMainGrid(GridDOMControl domControl)
        {
            var sb = new StringBuilder();

            var infos = Application.Client
                                  .SetDefaultCollection("Dashboard")
                                  .GetAll()
                                  .Select<GridInfo>();

            foreach(var info in infos)
            {
                sb.Append("<div>Title:").Append(info.Title).Append("</div>");
                sb.Append("<div>Comments:").Append(info.Comments.Count.ToString()).Append("</div>");
            }

            return sb.ToString();
        }
    }
}
