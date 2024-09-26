using System.Text;
using FractalPlatform.Client.App;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls.Component;

namespace FractalPlatform.Examples.Applications.OnlineShop
{
    public class RenderForm : ExtendedRenderForm
    {
        public RenderForm(BaseApplication application, DOMForm form) : base (application, form)
        {

        }

        public override string RenderComponent(ComponentDOMControl domControl)
        {
            if (domControl.ControlType == "Header")
            {
                var sb = new StringBuilder();

                sb.Append("<span>");
                sb.Append(domControl.Root["Category"].RenderComboBox()).Append("&nbsp;");
                sb.Append(domControl.Root["SearchText"].RenderTextBox()).Append("&nbsp;");
                sb.Append(domControl.Root["SearchButton"].RenderSubmitButton()).Append("&nbsp;");
                sb.Append(domControl.Root["NewCategory"].RenderLinkButton()).Append("&nbsp;");
                sb.Append(domControl.Root["Categories"].RenderLinkButton()).Append("&nbsp;");
                sb.Append(domControl.Root["NewProduct"].RenderLinkButton()).Append("&nbsp;");
                sb.Append(domControl.Root["Products"].RenderLinkButton()).Append("&nbsp;");
                sb.Append("</span>");

                return sb.ToString();
            }
            else if(domControl.ControlType == "Filters")
            {
                var sb = new StringBuilder();

                var prevName = string.Empty;

                foreach (var control in domControl.Root)
                {
                    if (control.Contains("Category"))
                    {
                        sb.Append("<div>")
                          .Append(control["Category"].RenderLinkButton())
                          .Append("</div>");
                    }
                    else
                    {
                        if (prevName != control["Name"].Value)
                        {
                            sb.Append("<div>")
                              .Append(control["Name"].RenderLabel())
                              .Append("</div>");
                        }

                        sb.Append("<div>")
                          .Append(control["Text"].RenderLabel())
                          .Append(control["Checked"].RenderCheckBox())
                          .Append("</div>");

                        prevName = control["Name"].Value;
                    }
                }

                return sb.ToString();
            }
            else if (domControl.ControlType == "Products")
            {
                var sb = new StringBuilder();

                foreach (var control in domControl.Root)
                {
                    sb.Append("<table border=1>");
                    sb.Append("<tr><td>Name:</td><td>").Append(control["Name"].RenderLabel()).Append("</td></tr>");
                    sb.Append("<tr><td>Description:</td><td>").Append(control["Description"].RenderLabel()).Append("</td></tr>");
                    sb.Append("<tr><td>Quantity:</td><td>").Append(control["Quantity"].RenderLabel()).Append("</td></tr>");
                    sb.Append("<tr><td>Price:</td><td>").Append(control["Price"].RenderLabel()).Append("</td></tr>");
                    sb.Append("<tr><td></td><td>").Append(control["OpenButton"].RenderLinkButton()).Append("</td></tr>");
                    sb.Append("</table>");
                }

                return sb.ToString();
            }

            return base.RenderComponent(domControl);
        }
    }

}
