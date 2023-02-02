using BigDoc.Client.UI.DOM.Controls;
using System.Text;
using BigDoc.Client.App;
using BigDoc.Client.UI.DOM.Controls.Grid;
using System.Data;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderForm: ExtendedRenderForm
    {
        public RenderForm(BaseApplication application) : base(application)
        {
        }

        public override string RenderMainGrid(GridDOMControl domControl)
        {
            foreach(DataRow dr in domControl.DataTable.Rows)
            {
                var title = dr["Title"].ToString();
                var onDate = dr["OnDate"].ToString();
                var who = dr["Who"].ToString();
                var picture = dr["Picture"].ToString();
                var text = dr["Text"].ToString();

                return "<div>" + title + "</div>";
            }

            return base.RenderMainGrid(domControl);
        }
    }
}
