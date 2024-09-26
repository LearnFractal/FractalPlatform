using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Database.Storages;

namespace FractalPlatform.Sandbox.Controls
{
    public class UploadFileControl : BaseControl
    {
        private Label _lblTextBox = new Label();
        private TextBox _tbTextBox = new TextBox();
        private Button _btnUploadFile = new Button();
             
        public UploadFileControl(MainForm parentForm, UploadFileDOMControl domControl) : base(parentForm, domControl)
        {
            // 
            // pnlFieldControl
            // 
            Controls.Add(_lblTextBox);
            Controls.Add(_tbTextBox);
            Controls.Add(_btnUploadFile);

            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlTextBox_" + domControl.Key);
            Size = new Size(domControl.Size.Width + 100, domControl.Size.Height);
            TabIndex = 1;
            Tag = domControl;

            // 
            // lblTextBox
            // 
            _lblTextBox.Dock = DockStyle.Left;
            _lblTextBox.Location = new Point(0, 0);
            _lblTextBox.Name = Helpers.EscapeName("lblTextBox_" + domControl.Key);
            _lblTextBox.Size = new Size(UIConstants.LabelWidth, domControl.HeightInPx);
            _lblTextBox.TabIndex = 0;
            _lblTextBox.Text = GetLocalizedValue(domControl.Key);

            // 
            // tbTextBox
            // 
            _tbTextBox.Location = new Point(UIConstants.LabelWidth, 0);
            _tbTextBox.Name = Helpers.EscapeName("tbTextBox_" + domControl.Key);
            _tbTextBox.Size = new Size(UIConstants.TextWidth, domControl.HeightInPx);
            _tbTextBox.TabIndex = 2;
            _tbTextBox.Text = domControl.Value;
            _tbTextBox.ReadOnly = true;

            // 
            // btnUploadFile
            // 
            _btnUploadFile.Location = new Point(UIConstants.LabelWidth + UIConstants.TextWidth + 10, 0);
            _btnUploadFile.Name = Helpers.EscapeName("btnUploadFile_" + domControl.Key);
            _btnUploadFile.Size = new Size(70, domControl.HeightInPx);
            _btnUploadFile.TabIndex = 1;
            _btnUploadFile.Text = "Upload";
            _btnUploadFile.UseVisualStyleBackColor = true;
            _btnUploadFile.Visible = true;
            _btnUploadFile.Tag = domControl;
            _btnUploadFile.Enabled = domControl.Enabled;

            _btnUploadFile.Click += btnUploadFile_Click;

            SetValidatonStyle();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            var btnUploadFile = (Button)sender;

            var domControl = (UploadFileDOMControl)btnUploadFile.Tag;

            var openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Any File|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileInfo = new FileInfo(openFileDialog.FileName);

                var filePath = GetFilePath();

                var newFileName = $"{Guid.NewGuid().ToString().Replace("-", "")}{fileInfo.Extension}";

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                var newFullName = System.IO.Path.Combine(filePath, newFileName);

                File.Copy(fileInfo.FullName, newFullName);

                _tbTextBox.Text = newFileName;

                domControl.OnValueChanged(new AttrValue(_tbTextBox.Text));
            }
        }
    }
}