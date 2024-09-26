using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Database.Storages;

namespace FractalPlatform.Sandbox.Controls
{
    public class NotSupportedControl : BaseControl
    {
        public NotSupportedControl(MainForm parentForm, DOMControl domControl) : base(parentForm, domControl)
        {
            var lblNotSupported = new Label();
            var lblTextBox = new Label();
            var tbTextBox = new TextBox();

            // 
            // pnlFieldControl
            // 
            Controls.Add(lblTextBox);
            Controls.Add(tbTextBox);
            Controls.Add(lblNotSupported);

            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlNotSupported_" + domControl.Key);
            Size = new Size(domControl.Size.Width + UIConstants.LabelWidth * 2, domControl.Size.Height);
            TabIndex = 1;
            Tag = domControl;

            // 
            // lblTextBox
            // 
            lblTextBox.Dock = DockStyle.Left;
            lblTextBox.Location = new Point(0, 0);
            lblTextBox.Name = Helpers.EscapeName("lblNotSupported_" + domControl.Key);
            lblTextBox.Size = new Size(UIConstants.LabelWidth, domControl.HeightInPx);
            lblTextBox.TabIndex = 0;
            lblTextBox.Text = GetLocalizedValue(domControl.Key);

            // 
            // tbTextBox
            // 
            tbTextBox.Location = new Point(UIConstants.LabelWidth, 0);
            tbTextBox.Name = Helpers.EscapeName("tbNotSupported_" + domControl.Key);
            tbTextBox.Size = new Size(UIConstants.TextWidth, domControl.HeightInPx);
            tbTextBox.TabIndex = 2;
            tbTextBox.Text = domControl.Value;
            tbTextBox.TextChanged += tbTextBox_TextChanged;
            tbTextBox.Enabled = domControl.Enabled && !domControl.ReadOnly;

            // 
            // lblNotSupported
            // 
            lblNotSupported.Location = new Point(UIConstants.LabelWidth + UIConstants.TextWidth + 10, 0);
            lblNotSupported.Name = Helpers.EscapeName("lblNotSupportedHeader_" + domControl.Key);
            lblNotSupported.Size = new Size(UIConstants.LabelWidth * 2, domControl.HeightInPx);
            lblNotSupported.TabIndex = 0;
            lblNotSupported.ForeColor = Color.Red;
            lblNotSupported.Text = "(Control not supported in sandbox)";

            SetValidatonStyle();
        }

        private void tbTextBox_TextChanged(object sender, EventArgs e)
        {
            var tbTextBox = (TextBox)sender;

            var domControl = (TextBoxDOMControl)DOMControl;

            domControl.OnValueChanged(new AttrValue(tbTextBox.Text));
        }
    }
}
