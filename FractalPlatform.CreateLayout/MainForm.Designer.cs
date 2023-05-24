namespace FractalPlatform.CreateLayout
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
            this.pnlNavigate = new System.Windows.Forms.Panel();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNavigate = new System.Windows.Forms.Button();
            this.tbNavigate = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.btnRemove = new System.Windows.Forms.Button();
            this.rtbOuterHtml = new System.Windows.Forms.RichTextBox();
            this.pnlNavigate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlNavigate
            // 
            this.pnlNavigate.Controls.Add(this.btnForward);
            this.pnlNavigate.Controls.Add(this.btnBack);
            this.pnlNavigate.Controls.Add(this.btnNavigate);
            this.pnlNavigate.Controls.Add(this.tbNavigate);
            this.pnlNavigate.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNavigate.Location = new System.Drawing.Point(0, 0);
            this.pnlNavigate.Margin = new System.Windows.Forms.Padding(1);
            this.pnlNavigate.MinimumSize = new System.Drawing.Size(339, 19);
            this.pnlNavigate.Name = "pnlNavigate";
            this.pnlNavigate.Size = new System.Drawing.Size(1182, 24);
            this.pnlNavigate.TabIndex = 0;
            // 
            // btnForward
            // 
            this.btnForward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnForward.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForward.Location = new System.Drawing.Point(805, -2);
            this.btnForward.Margin = new System.Windows.Forms.Padding(1);
            this.btnForward.MinimumSize = new System.Drawing.Size(33, 15);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(64, 25);
            this.btnForward.TabIndex = 3;
            this.btnForward.Text = ">>>";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnGoForward_Click);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.Location = new System.Drawing.Point(738, -1);
            this.btnBack.Margin = new System.Windows.Forms.Padding(1);
            this.btnBack.MinimumSize = new System.Drawing.Size(33, 15);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(65, 24);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "<<<";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // btnNavigate
            // 
            this.btnNavigate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNavigate.Location = new System.Drawing.Point(655, 1);
            this.btnNavigate.Margin = new System.Windows.Forms.Padding(1);
            this.btnNavigate.MinimumSize = new System.Drawing.Size(42, 14);
            this.btnNavigate.Name = "btnNavigate";
            this.btnNavigate.Size = new System.Drawing.Size(81, 24);
            this.btnNavigate.TabIndex = 1;
            this.btnNavigate.Text = "Navigate";
            this.btnNavigate.UseVisualStyleBackColor = true;
            this.btnNavigate.Click += new System.EventHandler(this.btnNavigate_Click);
            // 
            // tbNavigate
            // 
            this.tbNavigate.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbNavigate.Location = new System.Drawing.Point(0, 0);
            this.tbNavigate.Margin = new System.Windows.Forms.Padding(1);
            this.tbNavigate.Name = "tbNavigate";
            this.tbNavigate.Size = new System.Drawing.Size(653, 23);
            this.tbNavigate.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(1);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnRemove);
            this.splitContainer1.Panel2.Controls.Add(this.rtbOuterHtml);
            this.splitContainer1.Size = new System.Drawing.Size(1182, 695);
            this.splitContainer1.SplitterDistance = 870;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 1;
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = true;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Margin = new System.Windows.Forms.Padding(1);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(870, 695);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            this.webView.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            this.webView.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.webView_NavigationStarting);
            this.webView.WebMessageReceived += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs>(this.webView_WebMessageReceived);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(128, 191);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // rtbOuterHtml
            // 
            this.rtbOuterHtml.Location = new System.Drawing.Point(32, 79);
            this.rtbOuterHtml.Name = "rtbOuterHtml";
            this.rtbOuterHtml.Size = new System.Drawing.Size(275, 96);
            this.rtbOuterHtml.TabIndex = 0;
            this.rtbOuterHtml.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 695);
            this.Controls.Add(this.pnlNavigate);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.pnlNavigate.ResumeLayout(false);
            this.pnlNavigate.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlNavigate;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNavigate;
        private System.Windows.Forms.TextBox tbNavigate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.RichTextBox rtbOuterHtml;
        private System.Windows.Forms.Button btnRemove;
    }
}

