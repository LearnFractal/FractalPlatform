using System;
using System.Web.Helpers;
using System.Windows.Forms;

namespace FractalPlatform.CreateLayout
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            webView.Source = new Uri("https://finance.ua");
        }

        private async void webView_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var html = await webView.CoreWebView2.ExecuteScriptAsync("document.body.outerHTML");
            var htmldecoded = Json.Decode(html);
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            var script = @"document.addEventListener('contextmenu', function (event) {
                        let elem = document.elementFromPoint(event.clientX, event.clientY);
                        //alert(document.documentElement.outerHTML);
                        elem.style.backgroundColor = 'red';
                        let jsonObject =
                        {
                            innerHtml: elem.innerHTML,
                            ClientX: event.clientX,
                            ClientY: event.clientY
                        };
                        window.chrome.webview.postMessage(jsonObject);
                        event.preventDefault();
                    });";

            webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            webView.GoBack();
        }

        private void btnGoForward_Click(object sender, EventArgs e)
        {
            webView.GoForward();
        }

        private void webView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            tbNavigate.Text = e.Uri.ToString();
        }
    }
}