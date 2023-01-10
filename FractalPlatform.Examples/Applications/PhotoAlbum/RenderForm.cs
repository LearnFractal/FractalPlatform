using BigDoc.Client;
using BigDoc.Client.App;
using BigDoc.Client.UI.DOM.Controls.Grid;
using System.Data;
using System.Text;

namespace FractalPlatform.Examples.Applications.PhotoAlbum
{
    public class RenderForm: BaseRenderForm
    {
        public RenderForm(BaseApplication application) : base(application)
        {

        }

        public override string RenderGrid(GridDOMControl domControl)
        {
            if(domControl.Key == "Photos")
            {
                var sb = new StringBuilder();

                sb.Append("<tr><td collspan=2>");

                foreach(DataRow dr in domControl.DataTable.Rows)
                {
                    sb.Append(@$"<div align='left'>{dr["Title"]}</div>
                                 <div><img style='max-width:640px;max-height:640px'
                                           src='{GetFileUrl(dr["Photo"].ToString())}'>
                                 </div><br><br>");
                }

                sb.Append("</td></tr>");

                return sb.ToString();
            }
            else
            {
                return base.RenderGrid(domControl);
            }
        }
    }
}
