using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM.Controls;

namespace FractalPlatform.Sandbox.Controls
{
    public class LabelControl : BaseControl
    {
        public LabelControl(MainForm parentForm, LabelDOMControl domControl) : base(parentForm, domControl)
        {
            var lblLabel = new Label();
            
            // 
            // pnlCheckBoxControl
            // 
            Controls.Add(lblLabel);
            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlLabel_" + domControl.Key);
            Size = domControl.Size;
            TabIndex = 1;

            // 
            // lblLabel
            // 
            lblLabel.Dock = DockStyle.Left;
            lblLabel.Location = new Point(0, 0);
            lblLabel.Name = Helpers.EscapeName("lblLabel_" + domControl.Key);
            lblLabel.Size = domControl.Size;
            lblLabel.TabIndex = 0;
            lblLabel.Text = GetLocalizedValue(domControl.Value);
        }
    }
}
