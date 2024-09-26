
namespace FractalPlatform.Sandbox
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			pnlHeader = new System.Windows.Forms.Panel();
			btnFilter = new System.Windows.Forms.Button();
			lblTheme = new System.Windows.Forms.Label();
			cmbTheme = new System.Windows.Forms.ComboBox();
			lblLanguage = new System.Windows.Forms.Label();
			cmbLanguage = new System.Windows.Forms.ComboBox();
			tbFilter = new System.Windows.Forms.TextBox();
			pnlFooter = new System.Windows.Forms.Panel();
			btnNextPage = new System.Windows.Forms.Button();
			btnPrevPage = new System.Windows.Forms.Button();
			btnCancel = new System.Windows.Forms.Button();
			btnSave = new System.Windows.Forms.Button();
			pnlMain = new System.Windows.Forms.Panel();
			pnlHeader.SuspendLayout();
			pnlFooter.SuspendLayout();
			SuspendLayout();
			// 
			// pnlHeader
			// 
			pnlHeader.Controls.Add(btnFilter);
			pnlHeader.Controls.Add(lblTheme);
			pnlHeader.Controls.Add(cmbTheme);
			pnlHeader.Controls.Add(lblLanguage);
			pnlHeader.Controls.Add(cmbLanguage);
			pnlHeader.Controls.Add(tbFilter);
			pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
			pnlHeader.Location = new System.Drawing.Point(0, 0);
			pnlHeader.Name = "pnlHeader";
			pnlHeader.Size = new System.Drawing.Size(1182, 53);
			pnlHeader.TabIndex = 2;
			// 
			// btnFilter
			// 
			btnFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			btnFilter.Location = new System.Drawing.Point(1103, 13);
			btnFilter.Name = "btnFilter";
			btnFilter.Size = new System.Drawing.Size(66, 29);
			btnFilter.TabIndex = 7;
			btnFilter.Text = "Filter";
			btnFilter.UseVisualStyleBackColor = true;
			btnFilter.Click += btnFilter_Click;
			// 
			// lblTheme
			// 
			lblTheme.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			lblTheme.AutoSize = true;
			lblTheme.Location = new System.Drawing.Point(537, 19);
			lblTheme.Name = "lblTheme";
			lblTheme.Size = new System.Drawing.Size(57, 20);
			lblTheme.TabIndex = 6;
			lblTheme.Text = "Theme:";
			// 
			// cmbTheme
			// 
			cmbTheme.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			cmbTheme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cmbTheme.FormattingEnabled = true;
			cmbTheme.Items.AddRange(new object[] { "EN", "UA" });
			cmbTheme.Location = new System.Drawing.Point(599, 15);
			cmbTheme.Name = "cmbTheme";
			cmbTheme.Size = new System.Drawing.Size(98, 28);
			cmbTheme.TabIndex = 5;
			// 
			// lblLanguage
			// 
			lblLanguage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			lblLanguage.AutoSize = true;
			lblLanguage.Location = new System.Drawing.Point(704, 19);
			lblLanguage.Name = "lblLanguage";
			lblLanguage.Size = new System.Drawing.Size(77, 20);
			lblLanguage.TabIndex = 4;
			lblLanguage.Text = "Language:";
			// 
			// cmbLanguage
			// 
			cmbLanguage.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			cmbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			cmbLanguage.FormattingEnabled = true;
			cmbLanguage.Items.AddRange(new object[] { "EN", "UA" });
			cmbLanguage.Location = new System.Drawing.Point(782, 13);
			cmbLanguage.Name = "cmbLanguage";
			cmbLanguage.Size = new System.Drawing.Size(82, 28);
			cmbLanguage.TabIndex = 3;
			// 
			// tbFilter
			// 
			tbFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			tbFilter.Location = new System.Drawing.Point(885, 13);
			tbFilter.Name = "tbFilter";
			tbFilter.Size = new System.Drawing.Size(211, 27);
			tbFilter.TabIndex = 0;
			// 
			// pnlFooter
			// 
			pnlFooter.Controls.Add(btnNextPage);
			pnlFooter.Controls.Add(btnPrevPage);
			pnlFooter.Controls.Add(btnCancel);
			pnlFooter.Controls.Add(btnSave);
			pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			pnlFooter.Location = new System.Drawing.Point(0, 665);
			pnlFooter.Name = "pnlFooter";
			pnlFooter.Size = new System.Drawing.Size(1182, 58);
			pnlFooter.TabIndex = 3;
			// 
			// btnNextPage
			// 
			btnNextPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnNextPage.AutoSize = true;
			btnNextPage.Location = new System.Drawing.Point(858, 13);
			btnNextPage.Name = "btnNextPage";
			btnNextPage.Size = new System.Drawing.Size(94, 30);
			btnNextPage.TabIndex = 3;
			btnNextPage.Text = "Page >>";
			btnNextPage.UseVisualStyleBackColor = true;
			btnNextPage.Click += btnNextPage_Click;
			// 
			// btnPrevPage
			// 
			btnPrevPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnPrevPage.AutoSize = true;
			btnPrevPage.Location = new System.Drawing.Point(758, 13);
			btnPrevPage.Name = "btnPrevPage";
			btnPrevPage.Size = new System.Drawing.Size(94, 30);
			btnPrevPage.TabIndex = 2;
			btnPrevPage.Text = "<< Page";
			btnPrevPage.UseVisualStyleBackColor = true;
			btnPrevPage.Click += btnPrevPage_Click;
			// 
			// btnCancel
			// 
			btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnCancel.AutoSize = true;
			btnCancel.Location = new System.Drawing.Point(1075, 13);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new System.Drawing.Size(94, 30);
			btnCancel.TabIndex = 1;
			btnCancel.Text = "Cancel";
			btnCancel.UseVisualStyleBackColor = true;
			btnCancel.Click += btnCancel_Click;
			// 
			// btnSave
			// 
			btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			btnSave.AutoSize = true;
			btnSave.Location = new System.Drawing.Point(975, 13);
			btnSave.Name = "btnSave";
			btnSave.Size = new System.Drawing.Size(94, 30);
			btnSave.TabIndex = 0;
			btnSave.Text = "Save";
			btnSave.UseVisualStyleBackColor = true;
			btnSave.Click += btnSave_Click;
			// 
			// pnlMain
			// 
			pnlMain.AutoScroll = true;
			pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlMain.Location = new System.Drawing.Point(0, 53);
			pnlMain.Name = "pnlMain";
			pnlMain.Size = new System.Drawing.Size(1182, 612);
			pnlMain.TabIndex = 4;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1182, 723);
			Controls.Add(pnlMain);
			Controls.Add(pnlFooter);
			Controls.Add(pnlHeader);
			KeyPreview = true;
			Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			Name = "MainForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			Text = "Properties";
			Load += MainForm_Load;
			KeyUp += MainForm_KeyUp;
			pnlHeader.ResumeLayout(false);
			pnlHeader.PerformLayout();
			pnlFooter.ResumeLayout(false);
			pnlFooter.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TextBox tbFilter;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.ComboBox cmbLanguage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label lblTheme;
        private System.Windows.Forms.ComboBox cmbTheme;
    }
}