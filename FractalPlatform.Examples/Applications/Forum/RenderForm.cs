using BigDoc.Client;
using BigDoc.Client.App;
using BigDoc.Client.UI.DOM.Controls.Grid;
using System.Data;
using System.Text;

namespace FractalPlatform.Examples.Applications.Forum
{
    public class RenderForm: BaseRenderForm
    {
        public RenderForm(BaseApplication application) : base(application)
        {
        }

        public override string RenderGrid(GridDOMControl domControl)
        {
            if(domControl.Key == "Messages")
            {
                var sb = new StringBuilder();
                                
                foreach(DataRow dr in domControl.DataTable.Rows)
                {
                    sb.Append(@$"<tr>
                                    <td style='border:1px solid white' align='center'>
                                       <img style='max-width:120px;max-height:120px' src='{GetFileUrl(dr["Avatar"].ToString())}'>
                                       <div>{dr["Who"]}</div>
                                       <div>{dr["OnDate"]}</div>
                                    </td>
                                    <td  style='border:1px solid white' align='left' valign='top'>
                                        <div>{dr["Message"]}</div>
                                    </td>
                                 </tr>");
                }

                return sb.ToString();
            }
            else
            {
                return base.RenderGrid(domControl);
            }
        }
    }
}
