using System;
using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Database.Storages;

namespace FractalPlatform.Sandbox.Controls
{
    public class TextBoxControl : BaseControl
    {
        public TextBoxControl(MainForm parentForm, TextBoxDOMControl domControl) : base(parentForm, domControl)
        {
            var lblTextBox = new Label();
            var tbTextBox = new TextBox();
            
            // 
            // pnlFieldControl
            // 
            Controls.Add(lblTextBox);
            Controls.Add(tbTextBox);

            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlTextBox_" + domControl.Key);
            Size = new Size(domControl.Size.Width + 100, domControl.Size.Height);
            TabIndex = 1;
            Tag = domControl;
            
            // 
            // lblTextBox
            // 
            lblTextBox.Dock = DockStyle.Left;
            lblTextBox.Location = new Point(0, 0);
            lblTextBox.Name = Helpers.EscapeName("lblTextBox_" + domControl.Key);
            lblTextBox.Size = new Size(UIConstants.LabelWidth, domControl.HeightInPx);
            lblTextBox.TabIndex = 0;
            lblTextBox.Text = GetLocalizedValue(domControl.Key);

            // 
            // tbTextBox
            // 
            //tbTextBox.Dock = DockStyle.Fill;
            tbTextBox.Location = new Point(UIConstants.LabelWidth, 0);
            tbTextBox.Name = Helpers.EscapeName("tbTextBox_" + domControl.Key);
            tbTextBox.Size = new Size(UIConstants.TextWidth, domControl.HeightInPx);
            tbTextBox.TabIndex = 2;
            tbTextBox.Text = domControl.Value;
            tbTextBox.TextChanged += tbTextBox_TextChanged;
            tbTextBox.Enabled = domControl.Enabled;
            tbTextBox.ReadOnly = !domControl.HasSubForm && domControl.ReadOnly;

            if (domControl.IsCaptcha)
            {
                int width;
                domControl.GenerateCaptcha(out width);
                tbTextBox.Text = domControl.CaptchaValue;
            }
            else if (domControl.IsPassword)
            {
                tbTextBox.PasswordChar = '*';
            }

            // TODO: Create the list to use as the custom source. 
            //if (domControl.EnumItems != null &&
            //    domControl.EnumItems.Count > 0)
            //{
            //    var source = new AutoCompleteStringCollection();
            //    source.AddRange(domControl.EnumItems.ToArray());

            //    tbTextBox.AutoCompleteCustomSource = source;
            //    tbTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //    tbTextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //}

            if (domControl.HasSubForm)
            {
                var btnEditTextBox = new Button();

                // 
                // btnEditTextBox
                // 
                btnEditTextBox.Location = new Point(UIConstants.LabelWidth + UIConstants.TextWidth + 10, 0);
                btnEditTextBox.Name = Helpers.EscapeName("btnEditTextBox_" + domControl.Key);
                btnEditTextBox.Size = new Size(70, domControl.HeightInPx);
                btnEditTextBox.TabIndex = 1;
                btnEditTextBox.Text = "Edit";
                btnEditTextBox.UseVisualStyleBackColor = true;
                btnEditTextBox.Visible = domControl.HasSubForm;
                btnEditTextBox.Enabled = domControl.Enabled;
                btnEditTextBox.Tag = domControl;
                
                btnEditTextBox.Click += btnEditTextBox_Click;

                Controls.Add(btnEditTextBox);
            }

            SetValidatonStyle();
        }

        private void btnEditTextBox_Click(object sender, EventArgs e)
        {
            var btnEditTextBox = (Button)sender;

            var domControl = (TextBoxDOMControl)btnEditTextBox.Tag;

            domControl.OnSubFormClick(ParentForm.DomForm.Context.FormFactory);
        }

        private void tbTextBox_TextChanged(object sender, EventArgs e)
        {
            var tbTextBox = (TextBox)sender;

            var domControl = (TextBoxDOMControl)DOMControl;

            domControl.OnValueChanged(new AttrValue(tbTextBox.Text));
        }
    }
}
