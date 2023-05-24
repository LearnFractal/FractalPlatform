using Newtonsoft.Json;
using System;
using System.Text;
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

        private void Navigate(string url)
        {
            webView.Source = new Uri(url);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Navigate("https://sqlru.net");
        }

        private TagInfo _currentTagInfo;

        private async Task<string> ExecuteScript(string script)
        {
            var result = await webView.CoreWebView2.ExecuteScriptAsync(script);

            return Json.Decode(result);
        }

        private void AddSpaces(StringBuilder sb, int spaces)
        {
            for (int i = 0; i < spaces; i++)
            {
                sb.Append(' ');
            }
        }

        private string FormatHtml(string html)
        {
            var sb = new StringBuilder();

            var spaces = 0;

            char openedTag = ' ';

            int i = 0;

            for (; i < html.Length - 2; i++)
            {
                if (html[i] == '\n' || html[i] == '\r')
                    continue;

                if (html[i] == '<')
                {
                    if (i != 0)
                    {
                        sb.Append('\n');
                    }

                    if (html[i + 1] != '/')
                    {
                        AddSpaces(sb, spaces);

                        spaces += 3;

                        openedTag = html[i + 1];
                    }
                    else
                    {
                        if (html[i + 2] != openedTag)
                        {
                            spaces -= 3;
                        }

                        spaces -= 3;

                        AddSpaces(sb, spaces);
                    }
                }

                sb.Append(html[i]);

                if (html[i] == '>' && html[i + 1] != '<')
                {
                    sb.Append('\n');

                    AddSpaces(sb, spaces);
                }
            }

            sb.Append(html[i]);
            sb.Append(html[i + 1]);

            return sb.ToString();
        }

        private async void webView_WebMessageReceived(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebMessageReceivedEventArgs e)
        {
            var html = await ExecuteScript("document.documentElement.innerHTML");

            _currentTagInfo = JsonConvert.DeserializeObject<TagInfo>(e.WebMessageAsJson);

            rtbOuterHtml.Text = FormatHtml(_currentTagInfo.OuterHTML);

            var idx = html.IndexOf(_currentTagInfo.OuterHTML);
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            var script = @"let currElem = null; document.addEventListener('contextmenu', function (event) {
                        currElem = event.target;
                        currElem.style.backgroundColor = 'yellow';
                        let jsonObject =
                        {
                            OuterHTML: currElem.outerHTML
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

        private void btnNavigate_Click(object sender, EventArgs e)
        {
            Navigate(tbNavigate.Text);
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            await ExecuteScript($"currElem.remove();");
        }

        private async void btnMoveToParent_Click(object sender, EventArgs e)
        {
            var script = @"{
                            currElem = currElem.parentElement;
                            currElem.style.backgroundColor = 'yellow';
                            let jsonObject =
                            {
                                OuterHTML: currElem.outerHTML
                            };
                            window.chrome.webview.postMessage(jsonObject);
                           }";

            await ExecuteScript(script);
        }
    }
}