using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM.Controls;

namespace FractalPlatform.Sandbox.Controls
{
    public class LinkControl : BaseControl
    {
        public LinkControl(MainForm parentForm, LinkDOMControl domControl) : base(parentForm, domControl)
        {
            var lblLink = new LinkLabel();

            // 
            // lblLink
            // 
            Controls.Add(lblLink);

            Location = domControl.Location;
            Name = Helpers.EscapeName("lblLink" + domControl.Key);
            Size = domControl.Size;
            TabIndex = 1;

            // 
            // btnButton
            // 
            lblLink.Location = new Point(0, 0);
            lblLink.Name = Helpers.EscapeName("lblLink" + domControl.Key);
            lblLink.Size = domControl.Size;
            lblLink.TabIndex = 0;
            lblLink.Text = domControl.Value;
            lblLink.Tag = domControl;

            lblLink.Click += BtnLink_Click;
        }

        private void BtnLink_Click(object sender, System.EventArgs e)
        {
			Process.Start(new ProcessStartInfo
			{
				FileName = DOMControl.Value,
				UseShellExecute = true
			});
		}
    }
}
