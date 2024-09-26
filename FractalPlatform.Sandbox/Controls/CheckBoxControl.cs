using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Database.Storages;

namespace FractalPlatform.Sandbox.Controls
{
    public class CheckBoxControl : BaseControl
    {
        public CheckBoxControl(MainForm parentForm, CheckBoxDOMControl domControl) : base(parentForm, domControl)
            
        {
            var lblCheckBox = new Label();
            var chkCheckBox = new CheckBox();

            // 
            // pnlCheckBoxControl
            // 
            Controls.Add(chkCheckBox);
            Controls.Add(lblCheckBox);
            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlCheckBox_" + domControl.Key);
            Size = domControl.Size;
            TabIndex = 1;
            
            // 
            // lblCheckBox
            // 
            lblCheckBox.Dock = DockStyle.Left;
            lblCheckBox.Location = new Point(0, 0);
            lblCheckBox.Name = Helpers.EscapeName("lblCheckBox_" + domControl.Key);
            lblCheckBox.Size = new Size(UIConstants.LabelWidth, domControl.HeightInPx);
            lblCheckBox.TabIndex = 0;
            lblCheckBox.Text = GetLocalizedValue(domControl.Key);

            // 
            // chkCheckBox
            // 
            //chkCheckBox.Dock = DockStyle.Fill;
            chkCheckBox.Location = new Point(UIConstants.LabelWidth, 0);
            chkCheckBox.Name = Helpers.EscapeName("chkCheckBox_" + domControl.Key);
            chkCheckBox.Size = new Size(UIConstants.TextWidth, domControl.HeightInPx);
            chkCheckBox.TabIndex = 2;
            chkCheckBox.Enabled = domControl.Enabled && !domControl.ReadOnly;

            chkCheckBox.Checked = domControl.Value.Equals("true", StringComparison.InvariantCultureIgnoreCase);

            chkCheckBox.CheckedChanged += chkCheckBox_CheckedChanged;
            
            chkCheckBox.Tag = domControl;

            SetValidatonStyle();
        }

        private void chkCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var chkCheckBox = (CheckBox)sender;

            var domControl = (CheckBoxDOMControl)chkCheckBox.Tag;

            var attrValue = new AttrValue(chkCheckBox.Checked);

            domControl.OnValueChanged(attrValue);
        }
    }
}
