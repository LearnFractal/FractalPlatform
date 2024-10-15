using System.Drawing;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM.Controls;

namespace FractalPlatform.Sandbox.Controls
{
    public class GroupBoxControl : BaseControl
    {
        private GroupBox _grbGroupBox = new GroupBox();

        public GroupBoxControl(MainForm parentForm, GroupBoxDOMControl domControl) : base(parentForm, domControl)
        {
            Controls.Add(_grbGroupBox);

            // 
            // pnlParent
            // 
            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlParent_" + domControl.Key);
            TabIndex = 0;
            TabStop = false;
            AutoSize = true;

            if (domControl.Parent == null)
            {
                Size = domControl.Size;
            }
            else
            {
                Size = new Size(domControl.Size.Width - 20, domControl.Size.Height);
            }

            // 
            // grbGroupBox
            // 
            _grbGroupBox.Dock = DockStyle.Fill;
            _grbGroupBox.Location = new Point(0, 0);
            _grbGroupBox.Size = domControl.Size;
            _grbGroupBox.Name = Helpers.EscapeName("grbGroupBox_" + domControl.Key);
            _grbGroupBox.Text = GetLocalizedValue(domControl.Key);
            //_grbGroupBox.AutoSize = true;
        }

        public void AddControl(BaseControl control)
        {
            _grbGroupBox.Controls.Add(control);
        }
    }
}