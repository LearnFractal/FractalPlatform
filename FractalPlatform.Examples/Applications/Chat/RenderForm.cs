using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls.Grid;
using System.Data;
using System.Text;

namespace FractalPlatform.Examples.Applications.Chat
{
    public class RenderForm: BaseRenderForm
    {
        public RenderForm(BaseApplication application, DOMForm form) : base(application, form)
        {

        }

        public override string RenderGrid(GridDOMControl domControl)
        {
            if(domControl.Key == "Messages")
            {
                var sb = new StringBuilder();

                sb.Append("<tr><td collspan=2 nowrap><div align='left'>Chat messages:</div>");

                foreach(DataRow dr in domControl.DataTable.Rows)
                {
                    sb.Append($"<div align='left'>[{dr["OnDate"]}]&nbsp;{dr["Who"]}:&nbsp;{dr["Message"]}</div>");
                }

                sb.Append("<hr/>")
                  .Append("</td></tr>");

                return sb.ToString();
            }
            else
            {
                return base.RenderGrid(domControl);
            }
        }
    }
}
