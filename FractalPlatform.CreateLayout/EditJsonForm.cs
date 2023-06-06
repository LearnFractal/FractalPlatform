using System;
using System.Windows.Forms;

namespace FractalPlatform.CreateLayouts
{
    public partial class EditJsonForm : Form
    {
        public string Json => rtbJson.Text;

        public EditJsonForm(string json)
        {
            InitializeComponent();

            JsonHelpers.ValidateJson(json);

            rtbJson.Text = json;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                JsonHelpers.ValidateJson(rtbJson.Text);

                rtbJson.Text = JsonHelpers.FormatJson(rtbJson.Text);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
