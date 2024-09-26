using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM.Controls;

namespace FractalPlatform.Sandbox.Controls
{
    public class ButtonControl : BaseControl
    {
        public ButtonControl(MainForm parentForm, ButtonDOMControl domControl) : base(parentForm, domControl)
        {
            var btnButton = new Button();

            // 
            // btnButtonControl
            // 
            Controls.Add(btnButton);

            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlButton_" + domControl.Key);
            Size = domControl.Size;
            TabIndex = 1;

            // 
            // btnButton
            // 
            btnButton.Dock = DockStyle.Fill;
            btnButton.Location = new Point(0, 0);
            btnButton.Name = Helpers.EscapeName("btnButton_" + domControl.Key);
            btnButton.TabIndex = 0;
            btnButton.Text = GetLocalizedValue(domControl.Value);
            btnButton.Tag = domControl;
            btnButton.Enabled = domControl.Enabled;

            btnButton.Click += BtnButton_Click;
        }

        private void BtnButton_Click(object sender, System.EventArgs e)
        {
            var domControl = (ButtonDOMControl)((Button)sender).Tag;

            ParentForm.DomForm.SaveForm(domControl);

            domControl.OnClick();

            ParentForm.RefreshForm();
        }
    }
}
