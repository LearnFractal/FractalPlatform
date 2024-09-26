using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FractalPlatform.Sandbox.Controls.Grid;
using FractalPlatform.Sandbox.Controls;
using FractalPlatform.Client.UI.DOM;
using FractalPlatform.Client.UI.DOM.Controls;
using FractalPlatform.Client.UI.DOM.Controls.Grid;
using System.Diagnostics;

namespace FractalPlatform.Sandbox
{
    public partial class MainForm : Form
    {
        public DOMForm DomForm { get; }

        public MainForm(DOMForm domForm)
        {
            InitializeComponent();

            this.Text = $"{domForm.Name} form. Use sandbox only for local debug purposes. Ctrl + D deploy web application.";

            DomForm = domForm;

            Program.NeedRestartApp = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshMainForm();
        }

        private void AddControls(Control control, List<DOMControl> domControls, ref int maxWidth, ref int maxHeight)
        {
            foreach (var domControl in domControls)
            {
                if (!domControl.Visible)
                {
                    continue;
                }

                BaseControl subControl = null;

                if (domControl is LinkDOMControl)
                {
                    subControl = new LinkControl(this, (LinkDOMControl)domControl);
                }
                else if (domControl is ButtonDOMControl)
                {
                    subControl = new ButtonControl(this, (ButtonDOMControl)domControl);
                }
                else if (domControl is CheckBoxDOMControl)
                {
                    subControl = new CheckBoxControl(this, (CheckBoxDOMControl)domControl);
                }
                else if (domControl is ComboBoxDOMControl)
                {
                    subControl = new ComboBoxControl(this, (ComboBoxDOMControl)domControl);
                }
                else if (domControl is GroupBoxDOMControl)
                {
                    subControl = new GroupBoxControl(this, (GroupBoxDOMControl)domControl);
                }
                else if (domControl is LabelDOMControl)
                {
                    subControl = new LabelControl(this, (LabelDOMControl)domControl);
                }
                else if (domControl is TextBoxDOMControl)
                {
                    subControl = new TextBoxControl(this, (TextBoxDOMControl)domControl);
                }
                else if (domControl is RichTextBoxDOMControl)
                {
                    subControl = new RichTextBoxControl(this, (RichTextBoxDOMControl)domControl);
                }
                else if (domControl is UploadFileDOMControl)
                {
                    subControl = new UploadFileControl(this, (UploadFileDOMControl)domControl);
                }
                else if (domControl is TreeViewDOMControl)
                {
                    subControl = new TreeViewControl(this, (TreeViewDOMControl)domControl);
                }
                else if (domControl is GridDOMControl)
                {
                    subControl = new GridControl(this, (GridDOMControl)domControl);
                }
                else if (domControl is PictureDOMControl)
                {
                    subControl = new PictureControl(this, (PictureDOMControl)domControl);
                }
                else
                {
                    subControl = new NotSupportedControl(this, domControl);
                }

                subControl.Visible = domControl.Visible;

                subControl.Enabled = domControl.Enabled;

                if (control is GroupBoxControl)
                {
                    ((GroupBoxControl)control).AddControl(subControl);
                }
                else
                {
                    control.Controls.Add(subControl);
                }

                var width = subControl.Location.X + subControl.Width;

                if (width > maxWidth)
                {
                    maxWidth = width;
                }

                var height = subControl.Location.Y + subControl.Height;

                if (height > maxHeight)
                {
                    maxHeight = height;
                }

                AddControls(subControl, domControl.Controls, ref maxWidth, ref maxHeight);
            }
        }

        private void RefreshMainForm(bool needRefreshDomForm = true)
        {
            if (needRefreshDomForm)
            {
                DomForm.NeedRefreshForm = true;

                DomForm.RefreshForm();
            }

            this.pnlMain.SuspendLayout();

            this.pnlMain.Controls.Clear();

            int maxWidth = 0;
            int maxHeight = 0;

            AddControls(pnlMain, DomForm.Controls, ref maxWidth, ref maxHeight);

            InitBaseControls();

            this.Width = maxWidth + 20;
            this.Height = maxHeight + pnlHeader.Height + pnlFooter.Height + 50;

            this.pnlMain.ResumeLayout(false);
        }

        private void InitBaseControls()
        {
            btnPrevPage.Visible = DomForm.IsPrevPageButtonVisible;
            btnPrevPage.Enabled = DomForm.IsPrevPageButtonEnabled;
            btnPrevPage.Text = DomForm.PrevPageButtonText;

            btnNextPage.Visible = DomForm.IsNextPageButtonVisible;
            btnNextPage.Enabled = DomForm.IsNextPageButtonEnabled;
            btnNextPage.Text = DomForm.NextPageButtonText;

            btnSave.Visible = DomForm.IsSaveButtonVisible;
            btnSave.Text = DomForm.SaveButtonText;

            btnCancel.Visible = DomForm.IsCancelButtonVisible;
            btnCancel.Text = DomForm.CancelButtonText;

            var isFilterVisible = DomForm.IsFilterButtonVisible;

            tbFilter.Visible = isFilterVisible;
            btnFilter.Visible = isFilterVisible;

            //language
            var isLangVisible = DomForm.IsLanguageButtonVisible;

            lblLanguage.Visible = isLangVisible;
            cmbLanguage.Visible = isLangVisible;

            if (isLangVisible)
            {
                cmbLanguage.Items.Clear();

                foreach (var lang in DomForm.Languages)
                {
                    cmbLanguage.Items.Add(lang);
                }

                cmbLanguage.SelectedIndexChanged -= cmbLanguage_SelectedIndexChanged;
                
                cmbLanguage.SelectedItem = DomForm.Context.User.Language;

                cmbLanguage.SelectedIndexChanged += cmbLanguage_SelectedIndexChanged;
            }

            //theme
            var isThemeVisible = DomForm.IsThemeButtonVisible && ((DomForm.IsLoginPage && DomForm.IsChooseThemeOnLoginPage) || DomForm.IsChooseThemeOnAllPages);

            lblTheme.Visible = isThemeVisible;
            cmbTheme.Visible = isThemeVisible;

            if (isThemeVisible)
            {
                cmbTheme.Items.Clear();

                foreach (var theme in DomForm.Themes)
                {
                    cmbTheme.Items.Add(theme.ToString());
                }

                cmbTheme.SelectedIndex = (int)DomForm.GetUserTheme() - 1;
            }

            if (!isFilterVisible && !isLangVisible && !isThemeVisible)
            {
                pnlHeader.Height = 0;
            }

            if (!DomForm.IsPrevPageButtonVisible &&
               !DomForm.IsNextPageButtonVisible &&
               !DomForm.IsSaveButtonVisible &&
               !DomForm.IsCancelButtonVisible)
            {
                pnlFooter.Height = 0;
            }
        }

        public void RefreshForm()
        {
            if (DomForm.NeedRefreshForm)
            {
                RefreshMainForm();
            }
        }

        public void ReloadData()
        {
            if (DomForm.NeedReloadForm)
            {
                DomForm.ReloadData();
            }
        }

        private void CloseForm()
        {
            DomForm.Context.FormFactory.CloseForm();

            if (!DomForm.Context.FormFactory.HasForms)
            {
                Program.NeedRestartApp = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DomForm.SaveForm();

            if (!DomForm.HasValidationErrors)
            {
                this.DialogResult = DialogResult.OK;

                CloseForm();
            }
            else
            {
                RefreshMainForm(false);
            }
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            DomForm.SetUserLanguage(cmbLanguage.Text);

            RefreshMainForm();
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            DomForm.OnPrevPageClick();

            RefreshMainForm();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            DomForm.OnNextPageClick();

            RefreshMainForm();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            DomForm.Filter = tbFilter.Text;

            RefreshMainForm();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.D)
            {
#if DEBUG
                var workingDirectory = @$"{Utils.GetSolutionPath()}\FractalPlatform.Deployment\bin\Debug\net8.0";
#else
                var workingDirectory = @$"{Utils.GetSolutionPath()}\FractalPlatform.Deployment\bin\Release\net8.0";
#endif

                Process.Start(new ProcessStartInfo 
                { 
                    UseShellExecute = true,
                    WorkingDirectory = workingDirectory,
                    FileName= "FractalPlatform.Deployment.exe",
                    Arguments = DomForm.Context.Application.Name 
                });
            }
        }
    }
}