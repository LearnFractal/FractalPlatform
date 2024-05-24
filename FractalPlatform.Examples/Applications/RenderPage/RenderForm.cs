using System.Text;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;

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
