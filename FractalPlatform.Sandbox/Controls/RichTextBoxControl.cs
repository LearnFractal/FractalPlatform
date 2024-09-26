using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Database.Storages;

namespace FractalPlatform.Sandbox.Controls
{
    public class RichTextBoxControl : BaseControl
    {
        public RichTextBoxControl(MainForm parentForm, RichTextBoxDOMControl domControl) : base(parentForm, domControl)
        {
            var lblTextBox = new Label();
            var richTextBox = new RichTextBox();

            // 
            // pnlFieldControl
            // 
            Controls.Add(lblTextBox);
            Controls.Add(richTextBox);

            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlRichTextBox_" + domControl.Key);
            Size = domControl.Size;
            TabIndex = 1;
            Tag = domControl;

            // 
            // lblTextBox
            // 
            lblTextBox.Dock = DockStyle.Left;
            lblTextBox.Location = new Point(0, 0);
            lblTextBox.Name = Helpers.EscapeName("lblRichTextBox_" + domControl.Key);
            lblTextBox.Size = new Size(UIConstants.LabelWidth, domControl.HeightInPx);
            lblTextBox.TabIndex = 0;
            lblTextBox.Text = GetLocalizedValue(domControl.Key);

            // 
            // richTextBox
            // 
            richTextBox.Location = new Point(UIConstants.LabelWidth, 0);
            richTextBox.Name = Helpers.EscapeName("richTextBox_" + domControl.Key);
            richTextBox.Size = new Size(domControl.WidthInPx - UIConstants.LabelWidth, domControl.HeightInPx);
            richTextBox.TabIndex = 2;
            richTextBox.Text = domControl.Value;
            richTextBox.Enabled = domControl.Enabled;
            richTextBox.ReadOnly = domControl.ReadOnly;

            richTextBox.TextChanged += tbTextBox_TextChanged;

            SetValidatonStyle();
        }

        private void tbTextBox_TextChanged(object sender, EventArgs e)
        {
            var rtbTextBox = (RichTextBox)sender;

            var domControl = (RichTextBoxDOMControl)DOMControl;

            domControl.OnValueChanged(new AttrValue(rtbTextBox.Text));
        }
    }
}
