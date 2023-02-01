using BigDoc.Client.UI.DOM.Controls;
using System.Text;
using BigDoc.Client.App;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderForm: ExtendedRenderForm
    {
        public RenderForm(BaseApplication application) : base(application)
        {
        }

        public override string RenderLabel(LabelDOMControl domControl)
        {
            var sb = new StringBuilder();

            sb.Append("<div>").Append(domControl.Value).Append("</div>");

            return sb.ToString();
        }
    }
}
