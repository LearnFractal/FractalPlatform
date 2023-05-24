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
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitView = new System.Windows.Forms.SplitContainer();
            this.btnGoForward = new System.Windows.Forms.Button();
            this.btnGoBack = new System.Windows.Forms.Button();
            this.btnNavigate = new System.Windows.Forms.Button();
            this.tbNavigate = new System.Windows.Forms.TextBox();
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.btnRemove = new System.Windows.Forms.Button();
            this.rtbOuterHtml = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitView)).BeginInit();
            this.splitView.Panel1.SuspendLayout();
            this.splitView.Panel2.SuspendLayout();
            this.splitView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.splitView);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.btnRemove);
            this.splitMain.Panel2.Controls.Add(this.rtbOuterHtml);
            this.splitMain.Size = new System.Drawing.Size(1182, 695);
            this.splitMain.SplitterDistance = 857;
            this.splitMain.TabIndex = 0;
            // 
            // splitView
            // 
            this.splitView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitView.Location = new System.Drawing.Point(0, 0);
            this.splitView.Name = "splitView";
            this.splitView.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitView.Panel1
            // 
            this.splitView.Panel1.Controls.Add(this.btnGoForward);
            this.splitView.Panel1.Controls.Add(this.btnGoBack);
            this.splitView.Panel1.Controls.Add(this.btnNavigate);
            this.splitView.Panel1.Controls.Add(this.tbNavigate);
            // 
            // splitView.Panel2
            // 
            this.splitView.Panel2.Controls.Add(this.webView);
            this.splitView.Size = new System.Drawing.Size(857, 695);
            this.splitView.SplitterDistance = 42;
            this.splitView.TabIndex = 0;
            // 
            // btnGoForward
            // 
            this.btnGoForward.Location = new System.Drawing.Point(774, 10);
            this.btnGoForward.Name = "btnGoForward";
            this.btnGoForward.Size = new System.Drawing.Size(75, 23);
            this.btnGoForward.TabIndex = 3;
            this.btnGoForward.Text = ">>>";
            this.btnGoForward.UseVisualStyleBackColor = true;
            this.btnGoForward.Click += new System.EventHandler(this.btnGoForward_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(711, 10);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(56, 23);
            this.btnGoBack.TabIndex = 2;
            this.btnGoBack.Text = "<<<";
            this.btnGoBack.UseVisualStyleBackColor = true;
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // btnNavigate
            // 
            this.btnNavigate.Location = new System.Drawing.Point(630, 11);
            this.btnNavigate.Name = "btnNavigate";
            this.btnNavigate.Size = new System.Drawing.Size(75, 23);
            this.btnNavigate.TabIndex = 1;
            this.btnNavigate.Text = "Navigate";
            this.btnNavigate.UseVisualStyleBackColor = true;
            this.btnNavigate.Click += new System.EventHandler(this.btnNavigate_Click);
            // 
            // tbNavigate
            // 
            this.tbNavigate.Location = new System.Drawing.Point(4, 13);
            this.tbNavigate.Name = "tbNavigate";
            this.tbNavigate.Size = new System.Drawing.Size(620, 20);
            this.tbNavigate.TabIndex = 0;
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = true;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webView.Location = new System.Drawing.Point(0, 0);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(857, 649);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            this.webView.CoreWebView2InitializationCompleted += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs>(this.webView_CoreWebView2InitializationCompleted);
            this.webView.NavigationStarting += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs>(this.webView_NavigationStarting);
            this.webView.WebMessageReceived += new System.EventHandler<Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs>(this.webView_WebMessageReceived);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(118, 215);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 1;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // rtbOuterHtml
            // 
            this.rtbOuterHtml.Location = new System.Drawing.Point(36, 46);
            this.rtbOuterHtml.Name = "rtbOuterHtml";
            this.rtbOuterHtml.Size = new System.Drawing.Size(251, 135);
            this.rtbOuterHtml.TabIndex = 0;
            this.rtbOuterHtml.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 695);
            this.Controls.Add(this.splitMain);
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitView.Panel1.ResumeLayout(false);
            this.splitView.Panel1.PerformLayout();
            this.splitView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitView)).EndInit();
            this.splitView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitView;
        private System.Windows.Forms.Button btnGoForward;
        private System.Windows.Forms.Button btnGoBack;
        private System.Windows.Forms.Button btnNavigate;
        private System.Windows.Forms.TextBox tbNavigate;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.RichTextBox rtbOuterHtml;
    }
}

