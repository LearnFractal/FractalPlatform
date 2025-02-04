using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FractalPlatform.Client.UI;
using FractalPlatform.Client.UI.DOM.Controls;

namespace FractalPlatform.Sandbox.Controls
{
    public class PictureControl : BaseControl
    {
        public PictureControl(MainForm parentForm, PictureDOMControl domControl) : base(parentForm, domControl)
        {
            var bxPicture = new PictureBox();

            Controls.Add(bxPicture);
            Location = domControl.Location;
            Name = Helpers.EscapeName("pnlPicture_" + domControl.Key);
            TabIndex = 1;
            Size = domControl.Size;

            // 
            // bxPicture
            // 
            bxPicture.Dock = DockStyle.Fill;
            bxPicture.Location = new Point(0, 0);
            bxPicture.Name = Helpers.EscapeName("bxPicture_" + domControl.Key);
            bxPicture.TabIndex = 0;
            bxPicture.Size = domControl.Size;

            if (domControl.HasValue)
            {
                if (domControl.IsQRCode)
                {
                    bxPicture.Image = domControl.GenerateQRCodeImage();
                }
                else if (domControl.Value.StartsWith("http"))
                {
                    bxPicture.Load(domControl.Value);
                }
                else
                {
                    var filePath = Path.Combine(domControl.ParentForm.Context.Application.GetFilesPath(),
                                                domControl.Value);

                    bxPicture.Image = Image.FromFile(filePath);
                }
            }
        }
    }
}
