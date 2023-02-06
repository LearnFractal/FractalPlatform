using BigDoc.Client.UI.DOM.Controls;
using System.Text;
using BigDoc.Client.App;
using BigDoc.Client.UI.DOM;

namespace FractalPlatform.Examples.Applications.RenderPage
{
    public class RenderForm: ExtendedRenderForm
    {
        public RenderForm(BaseApplication application, DOMForm form) : base(application, form)
        {
        }

        public override string RenderLabel(LabelDOMControl domControl)
        {
            var sb = new StringBuilder();

            sb.Append("<div>").Append(domControl.Value).Append("</div>");

            return sb.ToString();
        }

        public override string RenderButton(ButtonDOMControl domControl)
        {
            var sb = new StringBuilder();

            sb.Append("<div>").Append(domControl.Value).Append("</div>");

            return sb.ToString();
        }
    }
}
