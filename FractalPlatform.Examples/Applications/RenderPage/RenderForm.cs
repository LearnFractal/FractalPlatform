using BigDoc.Client;
using BigDoc.Client.UI.DOM.Controls;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using BigDoc.Client.UI.DOM;
using BigDoc.Client.App;
using BigDoc.Client.UI.DOM.Controls.Component;
using System.Globalization;

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
