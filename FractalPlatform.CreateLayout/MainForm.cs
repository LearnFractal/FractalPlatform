using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Windows.Forms;

namespace FractalPlatform.CreateLayout
{
    public partial class MainForm : Form
    {
        public class TagInfo
        {
            public string OuterHTML { get; set; }

            public int ClientX { get; set; }

            public int ClientY { get; set; }
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            webView.Source = new Uri("https://finance.ua");
        }

        private TagInfo _currentTagInfo;

        private async Task<string> ExecuteScript(string script)
        {
            var result = await webView.CoreWebView2.ExecuteScriptAsync(script);

            return Json.Decode(result);
        }

        private async void webView_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var html = await ExecuteScript("document.documentElement.innerHTML");

            _currentTagInfo = JsonConvert.DeserializeObject<TagInfo>(e.WebMessageAsJson);

            var idx = html.IndexOf(_currentTagInfo.OuterHTML);
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            var script = @"document.addEventListener('contextmenu', function (event) {
                        let elem = document.elementFromPoint(event.clientX, event.clientY);
                        elem.style.backgroundColor = 'green';
                        let jsonObject =
                        {
                            OuterHTML: elem.outerHTML,
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