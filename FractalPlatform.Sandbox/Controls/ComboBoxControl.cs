using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Database.Storages;

namespace FractalPlatform.Sandbox.Controls
{
    public class ComboBoxControl : BaseControl
    {
        public ComboBoxControl(MainForm parentForm, ComboBoxDOMControl domControl) : base(parentForm, domControl)
        {
            var lblComboBox = new Label();
            var cmbComboBox = new ComboBox();

            // 
            // pnlCheckBoxControl
            // 
            Controls.Add(cmbComboBox);
            Controls.Add(lblComboBox);

            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlComboBox_" + domControl.Key);
            Size = domControl.Size;
            TabIndex = 1;

            // 
            // lblComboBox
            // 
            lblComboBox.Dock = DockStyle.Left;
            lblComboBox.Location = new Point(0, 0);
            lblComboBox.Name = Helpers.EscapeName("lblComboBox_" + domControl.Key);
            lblComboBox.Size = new Size(UIConstants.LabelWidth, domControl.HeightInPx);
            lblComboBox.TabIndex = 0;
            lblComboBox.Text = domControl.Key;

            // 
            // cmbComboBox
            // 
            cmbComboBox.Dock = DockStyle.Fill;
            cmbComboBox.Location = new Point(UIConstants.LabelWidth, 0);
            cmbComboBox.Name = Helpers.EscapeName("cmbComboBox_" + domControl.Key);
            cmbComboBox.Size = new Size(UIConstants.TextWidth, domControl.HeightInPx);
            cmbComboBox.TabIndex = 2;
            cmbComboBox.Text = GetLocalizedValue(domControl.Value);
            cmbComboBox.Enabled = domControl.Enabled && !domControl.ReadOnly;
            
            if (domControl.Items != null && domControl.Items.Count > 0)
            {
                cmbComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

                foreach (var item in domControl.Items)
                {
                    cmbComboBox.Items.Add(GetLocalizedValue(item));

                    if (item == domControl.Value)
                    {
                        cmbComboBox.SelectedIndex = cmbComboBox.Items.Count - 1;
                    }
                }
            }

            cmbComboBox.SelectedIndexChanged += cmbComboBox_SelectedIndexChanged;
            cmbComboBox.Tag = domControl;

            SetValidatonStyle();
        }

        private void cmbComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cmbComboBox = (ComboBox)sender;

            var domControl = (ComboBoxDOMControl)cmbComboBox.Tag;

            domControl.OnValueChanged(new AttrValue(cmbComboBox.Text));
        }
    }
}
