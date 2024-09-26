using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Microsoft.Web.WebView2.Core;
using FractalPlatform.CreateLayouts;

namespace FractalPlatform.CreateLayout
{
    public partial class MainForm : Form
    {
        #region Classes

        public class TagInfo
        {
            public string Id { get; set; }

            public string ParentId { get; set; }

            public string ControlAttr { get; set; }
        }

        #endregion

        #region Members

        private TagInfo _currentTagInfo;

        private string _documentFileName;

        private string _dbName;

        private string _collName;

        private bool _isProcessingComboBoxes = false;

        private bool _isRepeatableControl;

        private Options _options;

        private List<string> _undoUpdates = new List<string>();

        private int _currUndoUpdateIndex = -1;

        private string AppName => _dbName;

        private string CollName => _collName;

        private string RootPath => _documentFileName.Substring(0, _documentFileName.IndexOf("\\Databases"));

        private string FilesPath => $"{RootPath}\\files\\{_dbName}\\{_collName}";

        private string LayoutsPath => $"{RootPath}\\Layouts\\{_dbName}";

        private string LayoutHtmlPath => $"{LayoutsPath}\\{_collName}.html";

        #endregion

        #region Constructors

        public MainForm()
        {
            InitializeComponent();

            _options = ReadOptions();
        }

        #endregion

        #region Methods

        private Options ReadOptions()
        {
            var appSettings = File.ReadAllText("../../appsettings.json");

            return JsonConvert.DeserializeObject<Options>(appSettings);
        }

        private void ValidateOptions()
        {
            if (!File.Exists(_options.LayoutPath))
            {
                SaveOptions(string.Empty);
            }
        }

        private void SaveOptions(string layoutPath)
        {
            _options.LayoutPath = layoutPath;

            var appSettings = JsonConvert.SerializeObject(_options);

            appSettings = JsonHelpers.FormatJson(appSettings);

            File.WriteAllText("../../appsettings.json", appSettings);
        }

        private void Navigate(string path)
        {
            tbLayout.Text = path;

            var html = ReadHtml();

            html = HtmlHelpers.AddTagIdsToHtml(html);

            html = HtmlHelpers.ReplaceLinks(html, $"@BaseFilesUrl/", $"../../files/{_dbName}/");

            WriteHtml(html);

            webView.Source = new Uri(path);
        }

        private void RefreshPage()
        {
            var html = ReadHtml();

            html = HtmlHelpers.AddTagIdsToHtml(html);

            html = HtmlHelpers.ReplaceLinks(html, $"@BaseFilesUrl/", $"../../files/{_dbName}/");

            WriteHtml(html);

            webView.Reload();
        }

        private void RemoveOuterHtml()
        {
            _isRepeatableControl = false;

            rtbOuterHtml.Text = string.Empty;
        }

        private async Task<string> ExecuteScript(string script)
        {
            var result = await webView.CoreWebView2.ExecuteScriptAsync(script);

            return result; //Json.Decode(result);
        }

        private string ReadHtml()
        {
            return File.ReadAllText(tbLayout.Text);
        }

        private void WriteHtml(string html)
        {
            File.WriteAllText(tbLayout.Text, html);
        }

        private void AddUpdate(string html)
        {
            if (_currUndoUpdateIndex >= 0 &&
               _currUndoUpdateIndex < _undoUpdates.Count - 1)
            {
                _undoUpdates.RemoveRange(_currUndoUpdateIndex, _undoUpdates.Count - _currUndoUpdateIndex);
            }

            _undoUpdates.Add(html);

            _currUndoUpdateIndex = _undoUpdates.Count - 1;

            RefreshUndoRedoButtons();
        }

        private void RefreshUndoRedoButtons()
        {
            btnUndo.Enabled = _currUndoUpdateIndex > 0;
            btnRedo.Enabled = _currUndoUpdateIndex < _undoUpdates.Count - 1;
        }

        private void Apply()
        {
            var html = ReadHtml();

            html = HtmlHelpers.AddTagIdsToHtml(html);

            if (!_isRepeatableControl)
            {
                html = HtmlHelpers.ReplaceTagHtml(html, _currentTagInfo.Id, rtbOuterHtml.Text);
            }
            else //remove all items, but one
            {
                var parentHtml = HtmlHelpers.GetTagHtml(html, _currentTagInfo.ParentId);

                var leftParentHtml = parentHtml.Substring(0, parentHtml.IndexOf(">") + 1);

                var rightParentHtml = parentHtml.Substring(parentHtml.LastIndexOf("<"));

                html = HtmlHelpers.ReplaceTagHtml(html,
                                                 _currentTagInfo.ParentId,
                                                 leftParentHtml + rtbOuterHtml.Text + rightParentHtml);
            }

            html = HtmlHelpers.RemoveTagIds(html);

            WriteHtml(html);

            AddUpdate(html);

            RefreshPage();
        }

        private void SetControlText(string text, bool needSelection = false)
        {
            var idx = rtbOuterHtml.SelectionStart;

            var len = rtbOuterHtml.SelectionLength;

            if (needSelection && len == 0)
            {
                MessageBoxInfo("Need selection of text to change");

                return;
            }

            if (len > 0)
            {
                rtbOuterHtml.Text = rtbOuterHtml.Text.Remove(idx, len);
            }

            rtbOuterHtml.Text = rtbOuterHtml.Text.Insert(idx, text);

            if (!CreateControlTag())
            {
                return;
            }

            Apply();
        }

        private string AddAttrPrefixes(string html, string attrPrefix)
        {
            if (!string.IsNullOrEmpty(attrPrefix))
            {
                var reg = new Regex("@[a-zA-Z\\.]+");

                var matches = reg.Matches(rtbOuterHtml.Text);

                foreach (var match in matches)
                {
                    var str = match.ToString();

                    if (!str.Contains(".") &&
                         str != "@UserName" &&
                         str != "@BaseUrl" &&
                         str != "@BaseFilesUrl" &&
                         str != "@AddRowUrl" &&
                         str != "@EditRowUrl" &&
                         str != "@DeleteRowUrl")
                    {
                        var replacement = str.Replace("@", "@" + attrPrefix.Replace("\\", ".") + ".");

                        html = html.Replace(str, replacement);
                    }
                }
            }

            return html;
        }

        private void AddControlTag(string attr,
                                   bool isStandardType,
                                   bool isRepeatable)
        {
            var selectedText = rtbOuterHtml.SelectedText;

            var repeatable = string.Empty;

            if (isRepeatable)
            {
                repeatable = $" repeatable=\"true\"";
            }

            var html = rtbOuterHtml.Text;

            if (!isStandardType)
            {
                html = $"<control attr=\"{attr.Replace("\\", "\\\\")}\"{repeatable}>" + rtbOuterHtml.Text + "</control>";
            }
            else
            {
                html = $"<control attr=\"{attr.Replace("\\", "\\\\")}\" type=\"standard\"{repeatable}></control>";
            }

            rtbOuterHtml.Text = HtmlHelpers.FormatHtml(html);

            if (!string.IsNullOrEmpty(selectedText))
            {
                rtbOuterHtml.SelectionStart = rtbOuterHtml.Text.IndexOf(selectedText);
                rtbOuterHtml.SelectionLength = selectedText.Length;
            }
        }

        private string TrimSlashes(string str)
        {
            if (str.StartsWith("\\"))
            {
                return str.Substring(1);
            }
            else
            {
                return str;
            }
        }

        private bool CreateControlTag(bool isStandardType = false)
        {
            if (!EnsureLayout())
            {
                return false;
            }

            var json = File.ReadAllText(_documentFileName);

            var dlgChooseAttribute = new ChooseAttrForm(json, _currentTagInfo.ControlAttr);

            if (dlgChooseAttribute.ShowDialog() == DialogResult.OK)
            {
                var attr = dlgChooseAttribute.Attribute;

                string currPrefix;

                var attrParts = attr.Split(new string[] { "\\[0]" }, StringSplitOptions.None);

                if (attrParts.Length > 1)
                {
                    AddControlTag(TrimSlashes(attrParts[attrParts.Length - 2]), isStandardType, true); //no control tag, create controls one by one

                    currPrefix = TrimSlashes(attrParts[attrParts.Length - 1]);

                    _isRepeatableControl = true;
                }
                else
                {
                    AddControlTag(string.Empty, isStandardType, false); //no control tag

                    currPrefix = TrimSlashes(attrParts[0]);
                }

                rtbOuterHtml.Text = AddAttrPrefixes(rtbOuterHtml.Text, currPrefix);

                return true;
            }
            else
            {
                return false;
            }
        }

        private MatchCollection GetMatchedAttrs(string html)
        {
            var reg = new Regex("(?<AttrName>[a-zA-Z]+)[\\s]*=[\\s]*[\"\'](?<Value>.*?)[\"\']");

            return reg.Matches(html);
        }

        private void BindControl(string[] tagNames,
                                 string nameAlias,
                                 string onClickAlias,
                                 string onClickUrlAlias,
                                 string onChangeAlias,
                                 string valueAlias,
                                 string[] removeWords = null,
                                 string[] addAliases = null,
                                 string addTags = null)
        {
            var text = rtbOuterHtml.SelectedText;

            var bFind = false;

            var tagName = string.Empty;

            for (int i = 0; i < tagNames.Length; i++)
            {
                if (text.StartsWith("<" + tagNames[i]))
                {
                    bFind = true;

                    tagName = tagNames[i];

                    break;
                }
            }

            if (!bFind)
            {
                MessageBoxInfo($"Text should be started from {string.Join(",", tagNames)} tags");

                return;
            }

            if (!text.EndsWith(">"))
            {
                MessageBoxInfo("Text should be ended with </input> tag");

                return;
            }

            var isNameExists = false;
            var isOnClickExists = false;
            var isOnClickUrlExists = false;
            var isOnChangeExists = false;
            var isValueExists = false;

            foreach (Match item in GetMatchedAttrs(text))
            {
                var attr = item.Groups["AttrName"].Value;
                var val = item.Groups["Value"].Value;

                if (attr == "name")
                {
                    if (!string.IsNullOrEmpty(nameAlias))
                    {
                        text = text.Replace(val, nameAlias);
                    }

                    isNameExists = true;
                }
                else if (attr == "onclick" &&
                         (tagName == "input" || tagName == "button"))
                {
                    if (!string.IsNullOrEmpty(onClickAlias))
                    {
                        text = text.Replace(val, onClickAlias);
                    }

                    isOnClickExists = true;
                }
                else if (attr == "onchange")
                {
                    if (!string.IsNullOrEmpty(onChangeAlias))
                    {
                        text = text.Replace(val, onChangeAlias);
                    }

                    isOnChangeExists = true;
                }
                else if (attr == "value")
                {
                    if (!string.IsNullOrEmpty(valueAlias))
                    {
                        text = text.Replace(val, valueAlias);
                    }

                    isValueExists = true;
                }
                else if (attr == "href" &&
                        tagName == "a")
                {
                    if (!string.IsNullOrEmpty(onClickUrlAlias))
                    {
                        text = text.Replace(val, onClickUrlAlias);
                    }

                    isOnClickUrlExists = true;
                }
            }

            var idx = text.IndexOf('>');

            if (!isNameExists && !string.IsNullOrEmpty(nameAlias))
            {
                text = text.Insert(idx, $" name=\"{nameAlias}\"");
            }

            if (!isOnClickExists && !string.IsNullOrEmpty(onClickAlias) &&
                (tagName == "input" || tagName == "button"))
            {
                text = text.Insert(idx, $" onclick=\"{onClickAlias}\"");
            }

            if (!isOnClickUrlExists && !string.IsNullOrEmpty(onClickUrlAlias) &&
                tagName == "a")
            {
                text = text.Insert(idx, $" href=\"{onClickUrlAlias}\"");
            }

            if (!isOnChangeExists && !string.IsNullOrEmpty(onChangeAlias))
            {
                text = text.Insert(idx, $" onchange=\"{onChangeAlias}\"");
            }

            if (!isValueExists && !string.IsNullOrEmpty(valueAlias))
            {
                text = text.Insert(idx, $" value=\"{valueAlias}\"");
            }

            if (removeWords != null)
            {
                foreach (var removeWord in removeWords)
                {
                    text = text.Replace(removeWord, string.Empty);
                }
            }

            if (addAliases != null)
            {
                foreach (var addAlias in addAliases)
                {
                    text = text.Insert(idx, ' ' + addAlias);
                }
            }

            if (!string.IsNullOrEmpty(addTags))
            {
                text += "\r\n" + addTags;
            }

            rtbOuterHtml.Text = rtbOuterHtml.Text.Remove(rtbOuterHtml.SelectionStart, rtbOuterHtml.SelectionLength);
            rtbOuterHtml.Text = rtbOuterHtml.Text.Insert(rtbOuterHtml.SelectionStart, text);

            if (!CreateControlTag())
            {
                return;
            }

            Apply();
        }

        private void RefreshComboBoxes()
        {
            if (_isProcessingComboBoxes)
                return;

            _isProcessingComboBoxes = true;

            var idx = _documentFileName.LastIndexOf("Databases") + "Databases".Length;

            var databasesPath = _documentFileName.Substring(0, idx);

            //clear databases
            cbDatabase.Items.Clear();

            foreach (var dir in new DirectoryInfo(databasesPath).GetDirectories())
            {
                cbDatabase.Items.Add(dir.Name);

                if (_documentFileName.StartsWith(dir.FullName))
                {
                    _dbName = dir.Name;
                }
            }

            cbDatabase.SelectedItem = _dbName;

            //clear collections
            cbCollection.Items.Clear();

            foreach (var dir in new DirectoryInfo($"{databasesPath}\\{_dbName}").GetDirectories())
            {
                cbCollection.Items.Add(dir.Name);

                if (_documentFileName.StartsWith(dir.FullName))
                {
                    _collName = dir.Name;
                }
            }

            cbCollection.SelectedItem = _collName;

            _isProcessingComboBoxes = false;
        }

        private bool EnsureDocument(bool isForce = false)
        {
            if (cbCollection.Items.Count > 0 && !isForce)
            {
                return true;
            }

            MessageBoxInfo("Choose a 0000000001.json document in a database collection");

            openJsonFileDialog.InitialDirectory = @"C:\FractalPlatform\FractalPlatform.Examples\Databases";

            if (openJsonFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openJsonFileDialog.FileName;

                if (!fileName.Contains("Databases") || !fileName.Contains("Document"))
                {
                    MessageBoxInfo("Choose a 0000000001.json document in a database collection");

                    return false;
                }

                _documentFileName = openJsonFileDialog.FileName;

                RefreshComboBoxes();

                return true;
            }
            else
            {
                return false;
            }
        }

        private bool EnsureLayout(bool isShowInfo = true)
        {
            if (!string.IsNullOrEmpty(tbLayout.Text))
            {
                return true;
            }

            if (isShowInfo)
            {
                MessageBoxInfo("You should choose a layout to navigate.");
            }

            if (openHtmlFileDialog.ShowDialog() == DialogResult.OK)
            {
                var fileName = openHtmlFileDialog.FileName;

                if (!fileName.Contains("Layouts"))
                {
                    MessageBoxInfo("Choose a layout html file");

                    return false;
                }

                _documentFileName = openHtmlFileDialog.FileName
                                            .Replace("Layouts", "Databases")
                                            .Replace(".html", @"\Document\0000000001.json");

                SaveOptions(LayoutHtmlPath);

                RefreshComboBoxes();

                Navigate(openHtmlFileDialog.FileName);

                return true;
            }
            else
            {
                return false;
            }
        }

        private string GetDimensionPath(string dimension)
        {
            var path = _documentFileName;

            if (dimension != "Document")
            {
                path = path.Replace("Document", dimension + "\\Document").Replace("0000000001.json", "0000000000.json");
            }

            return path;
        }

        private void ChangeUILayout(string layout)
        {
            var path = GetDimensionPath("UI");

            if (!File.Exists(path))
            {
                var fileInfo = new FileInfo(path);

                if (!Directory.Exists(fileInfo.DirectoryName))
                {
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                }

                File.WriteAllText(path, "{}");
            }

            var json = File.ReadAllText(path);

            var pattern = "[\"']Layout[\"'][\\s]*:[\\s]*[\"\'].*?[\"\']";

            if (!string.IsNullOrEmpty(layout))
            {
                var attr = $"\"Layout\": \"{layout}\"";

                if (json.Contains("\"Layout\""))
                {
                    json = Regex.Replace(json, pattern, attr);
                }
                else
                {
                    if (json.Length > 2) //not empty {} json, insert with quote
                    {
                        json = json.Insert(1, attr + ",");
                    }
                    else
                    {
                        json = json.Insert(1, attr);
                    }
                }
            }
            else
            {
                json = Regex.Replace(json, pattern, string.Empty);
            }

            json = JsonHelpers.FormatJson(json);

            File.WriteAllText(path, json);
        }

        private void EditDimension(string dimension)
        {
            var path = GetDimensionPath(dimension);

            if (!File.Exists(path))
            {
                MessageBoxInfo($"'{path}' file is not exists");

                return;
            }

            var json = File.ReadAllText(path);

            var dlgEditJson = new EditJsonForm(json);

            if (dlgEditJson.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(path, dlgEditJson.Json);
            }
        }

        private void MessageBoxInfo(string message)
        {
            MessageBox.Show(message, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool MessageBoxQuestion(string message)
        {
            return MessageBox.Show(message,
                                   "Question",
                                   MessageBoxButtons.OKCancel,
                                   MessageBoxIcon.Question) == DialogResult.OK;
        }

        #endregion

        #region Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            ValidateOptions();

            if (!string.IsNullOrEmpty(_options.LayoutPath))
            {
                _documentFileName = _options.LayoutPath
                                        .Replace("Layouts", "Databases")
                                        .Replace(".html", @"\Document\0000000001.json");

                RefreshComboBoxes();

                Navigate(_options.LayoutPath);

                var html = ReadHtml();

                AddUpdate(html);
            }
        }

        private void webView_WebMessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            _currentTagInfo = JsonConvert.DeserializeObject<TagInfo>(e.WebMessageAsJson);

            var html = ReadHtml();

            html = HtmlHelpers.AddTagIdsToHtml(html);

            html = HtmlHelpers.GetTagHtml(html, _currentTagInfo.Id);

            html = HtmlHelpers.RemoveTagIds(html);

            RemoveOuterHtml();

            rtbOuterHtml.Text = HtmlHelpers.FormatHtml(html);

            //rtbOuterHtml.Text = RemoveColorAttrs(rtbOuterHtml.Text);

            rtbOuterHtml.Focus();

            rtbOuterHtml.SelectAll();
        }

        private void webView_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            var script = @"let currElems = []; 
                        let prevElem = null; 
                        let prevColor = null;

                        function getControlAttr(elem)
                        {
                            var controlAttr = '';

                            var parentElement = elem;

                            while(parentElement != null)
                            {
                                if(parentElement.tagName == 'CONTROL')
                                {
                                   var attr = parentElement.getAttribute('attr');
                                   var repeatable = parentElement.getAttribute('repeatable');

                                   if(attr != '')
                                   {
                                        if(repeatable == 'true')
                                        {
                                            if(controlAttr != '')
                                            {
                                                controlAttr = attr + '\\[0]\\' + controlAttr;
                                            }
                                            else
                                            {
                                                controlAttr = attr + '\\[0]';
                                            }
                                        }
                                        else
                                        {
                                            if(controlAttr != '')
                                            {
                                                controlAttr = attr + '\\' + controlAttr;
                                            }
                                            else
                                            {
                                                controlAttr = attr;
                                            }
                                        }
                                   }
                                }

                                parentElement = parentElement.parentElement;
                            }

                            return controlAttr;
                        }
                        
                        document.addEventListener('contextmenu', function (event) {
                            let currElem = event.target;

                            if(currElem.parentElement.tagName == 'CONTROL')
                            {
                                currElem = currElem.parentElement;
                            }

                            currElems.push(currElem);

                            if(currElem.style.backgroundColor != 'lightGreen' &&
                               currElem.tagName != 'CONTROL')
                            {
                                prevColor = currElem.style.backgroundColor;

                                currElem.style.backgroundColor = 'yellow';
                            }

                            if(prevElem != null)
                            {
                                if(prevElem.style.backgroundColor != 'lightGreen')
                                {
                                    prevElem.style.backgroundColor = prevColor;    
                                }
                            }

                            prevElem = currElem;
                            
                            let jsonObject =
                            {
                                Id: currElem.id,
                                ControlAttr: getControlAttr(currElem),
                                ParentId: currElem.parentElement.id
                            };

                            window.chrome.webview.postMessage(jsonObject);
                            event.preventDefault();
                        });";

            webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script);
        }

        private async void webView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            var script = @"{
                              let currElems = document.getElementsByTagName('control');

                              for(var i=0; i<currElems.length; i++)
                              {
                                for(var j=0; j<currElems[i].children.length; j++)
                                {
                                    currElems[i].children[j].style.backgroundColor = 'lightGreen';
                                }
                              }
                           }";

            await ExecuteScript(script);

            var html = ReadHtml();

            html = HtmlHelpers.RemoveTagIds(html);

            html = HtmlHelpers.ReplaceLinks(html, $"../../files/{_dbName}/", $"@BaseFilesUrl/");

            WriteHtml(html);

            RemoveOuterHtml();

            _isRepeatableControl = false;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveOuterHtml();

            Apply();
        }

        private async void btnMoveToParent_Click(object sender, EventArgs e)
        {
            var script = @"{
                            let currElem = currElems[currElems.length-1].parentElement;
                            currElems.push(currElem);
                            
                            if(currElem.style.backgroundColor != 'lightGreen' &&
                               currElem.tagName != 'CONTROL')
                            {
                                prevColor = currElem.style.backgroundColor;

                                currElem.style.backgroundColor = 'yellow';
                            }

                            if(prevElem != null)
                            {
                                if(prevElem.style.backgroundColor != 'lightGreen')
                                {
                                    prevElem.style.backgroundColor = prevColor;    
                                }
                            }

                            prevElem = currElem;

                            let jsonObject =
                            {
                                Id: currElem.id,
                                ControlAttr: getControlAttr(currElem),
                                ParentId: currElem.parentElement.id
                            };
                            
                            window.chrome.webview.postMessage(jsonObject);}";

            await ExecuteScript(script);
        }

        private async void btnMoveToChild_Click(object sender, EventArgs e)
        {
            var script = @"{
                            currElems.pop();

                            let currElem = currElems[currElems.length-1];
                            
                            if(currElem.style.backgroundColor != 'lightGreen' &&
                               currElem.tagName != 'CONTROL')
                            {
                                prevColor = currElem.style.backgroundColor;

                                currElem.style.backgroundColor = 'yellow';
                            }

                            if(prevElem != null)
                            {
                                if(prevElem.style.backgroundColor != 'lightGreen')
                                {
                                    prevElem.style.backgroundColor = prevColor;    
                                }
                            }

                            prevElem = currElem;

                            let jsonObject =
                            {
                                Id: currElem.id,
                                ControlAttr: getControlAttr(currElem),
                                ParentId: currElem.parentElement.id
                            };
                            
                            window.chrome.webview.postMessage(jsonObject);}";

            await ExecuteScript(script);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void valueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Value", true);
        }

        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Name");
        }

        private void enabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Enabled");
        }

        private void disabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Disabled");
        }

        private void checkedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Checked");
        }

        private void readOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@ReadOnly");
        }

        private void toolTipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@ToolTip");
        }

        private void keyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Key");
        }

        private void widthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Width");
        }

        private void heightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Height");
        }

        private void visibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@Visible");
        }

        private void clickUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@ClickUrl");
        }

        private void clickSubmitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@ClickSubmit");
        }

        private void filterTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@FilterText");
        }

        private void addRowUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@AddRowUrl");
        }

        private void editRowUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@EditRowUrl");
        }

        private void deleteRowUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@DeleteRowUrl");
        }

        private void comboBoxItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@ComboBoxItems");
        }

        private void savePageScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@SavePageScript");
        }

        private void updatePageScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@UpdatePageScript");
        }

        private void cancelPageScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@CancelPageScript");
        }

        private void filterPageScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@FilterPageScript");
        }

        private void savePageVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@SavePageVisible");
        }

        private void cancelPageVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@CancelPageScript");
        }

        private void savePageTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@SavePageText");
        }

        private void cancelPageTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@CancelPageText");
        }

        private void nextPageVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@NextPageScript");
        }

        private void prevPageVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@PrevPageScript");
        }

        private void nextPageEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@NextPageEnabled");
        }

        private void prevPageEnabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@PrevPageEnabled");
        }

        private void nextPageDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@NextPageDisabled");
        }

        private void prevPageDisabledToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@PrevPageDisabled");
        }

        private void userNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText("@UserName");
        }

        private void createLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<span>@Value</span>");
        }

        private void createTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""text"" name=""@Name"" value=""@Value"" @Disabled @ReadOnly/>");
        }

        private void createButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""button"" name=""@Name"" value=""@Value"" @Disabled @ReadOnly onclick=""@ClickSubmit""/>");
        }

        private void createCheckBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""checkbox"" value=""@Value"" @Disabled @ReadOnly @Checked onchange=""checkedChanged(this, '@Name')""/>
                             <input type=""hidden"" name=""@Name"" value=""@Value""/>");
        }

        private void createComboBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<select name=""@Name"" @Disabled>@ComboBoxItems</select>");
        }

        private void createComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CreateControlTag())
            {
                return;
            }

            Apply();
        }

        private void createStandardComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CreateControlTag(true))
            {
                return;
            }

            Apply();
        }

        private void createSavePageButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""button"" name=""btnSave"" value=""@SavePageText"" onclick=""@SavePageScript""/>");
        }

        private void createCancelPageButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""button"" name=""btnCancel"" value=""@CancelPageText"" onclick=""@CancelPageScript""/>");
        }

        private void createPrevPageButtonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""button"" name=""btnPrevPage"" value=""Prev"" @PrevPageDisabled onclick=""@PrevPageScript""/>");
        }

        private void createNextPageButtonToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""button"" name=""btnNextPage"" value=""Next"" @NextPageDisabled onclick=""@NextPageScript""/>");
        }

        private void createFilterPageButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""button"" name=""btnFilter"" value=""Filter"" onclick=""@FilterPageScript""/>");
        }

        private void createFilterTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetControlText(@"<input type=""text"" name=""FilterText"" value=""@FilterText""/>");
        }

        private void bindLabelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //any text
            SetControlText(@"<span>@Value</span>");
        }

        private void bindTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=text
            BindControl(new[] { "input" },
                        "@Name",
                        null,
                        null,
                        null,
                        "@Value",
                        new[] { "disabled", "readonly" },
                        new[] { "@Disabled", "@ReadOnly" });
        }

        private void bindButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=button or <button>
            BindControl(new[] { "input", "button", "a" },
                        "@Name",
                        "@ClickSubmit",
                        "@ClickUrl",
                        null,
                        "@Value",
                        new[] { "disabled", "readonly" },
                        new[] { "@Disabled", "@ReadOnly" });
        }

        private void bindCheckBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=checkbox
            BindControl(new[] { "input" },
                        "@Name",
                        null,
                        null,
                        "checkedChanged(this,'@Name')",
                        "@Value",
                        new[] { "disabled", "readonly" },
                        new[] { "@Disabled", "@ReadOnly" },
                        @"<input type=""hidden"" name=""@Name"" value=""@Value""/>");
        }

        private void bindComboBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //select
            BindControl(new[] { "select" },
                        "@Name",
                        null,
                        null,
                        null,
                        null,
                        new[] { "disabled" },
                        new[] { "@Disabled" },
                        "@ComboBoxItems");
        }

        private void bindComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CreateControlTag())
            {
                return;
            }

            Apply();
        }

        private void bindSaveButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=button or <button>
            BindControl(new[] { "input", "button" },
                        "btnSave",
                        "@SavePageScript",
                        "@SavePageUrl",
                        null,
                        "@SavePageText",
                        null,
                        new[] { "@SavePageVisible" });
        }

        private void bindCancelButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=button or <button>
            BindControl(new[] { "input", "button" },
                        "btnCancel",
                        "@CancelPageScript",
                        "@CancelPageUrl",
                        null,
                        "@CancelPageText",
                        null,
                        new[] { "@CancelPageVisible" });
        }

        private void bindPrevPageButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=button or <button>
            BindControl(new[] { "input", "button" },
                        "btnPrevPage",
                        "@PrevPageScript",
                        "@PrevPageUrl",
                        null,
                        "Prev",
                        new[] { "disabled" },
                        new[] { "@PrevPageDisabled", "@PrevPageVisible" });
        }

        private void bindNextPageButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=button or <button>
            BindControl(new[] { "input", "button" },
                        "btnNextPage",
                        "@NextPageScript",
                        "@NextPageUrl",
                        null,
                        "Next",
                        new[] { "disabled" },
                        new[] { "@NextPageDisabled", "@NextPageVisible" });
        }

        private void bindFilterPageButtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=button or <button>
            BindControl(new[] { "input", "button" },
                        "btnFilter",
                        "@FilterPageScript",
                        "@FilterPageUrl",
                        null,
                        "Filter");
        }

        private void bindFilterTextBoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //input type=button or <button>
            BindControl(new[] { "input", "button" },
                        "FilterText",
                        null,
                        null,
                        null,
                        "@FilterText");
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            EnsureLayout(false);
        }

        private void cbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            _documentFileName = _documentFileName.Replace($"\\{_dbName}\\", $"\\{cbDatabase.SelectedItem}\\");

            RefreshComboBoxes();
        }

        private void cbCollection_SelectedIndexChanged(object sender, EventArgs e)
        {
            _documentFileName = _documentFileName.Replace($"\\{_collName}\\", $"\\{cbCollection.SelectedItem}\\");

            RefreshComboBoxes();

            if (cbCollection.SelectedItem != null)
            {
                if (!File.Exists(LayoutHtmlPath) &&
                    MessageBoxQuestion($"Do you want to copy layout for '{cbCollection.SelectedItem}' collection ?"))
                {
                    if (!Directory.Exists(LayoutsPath))
                    {
                        Directory.CreateDirectory(LayoutsPath);
                    }

                    FileHelpers.CopyFilesRecursively(_options.FilesPath, FilesPath);

                    var html = File.ReadAllText(_options.LayoutPath);

                    html = HtmlHelpers.ReplaceLinks(html, $"@BaseFilesUrl/{_options.CollName}/", $"@BaseFilesUrl/{CollName}/");

                    File.WriteAllText(LayoutHtmlPath, html);

                    SaveOptions(LayoutHtmlPath);

                    ChangeUILayout(_collName);

                    Navigate(_options.LayoutPath);
                }
            }
        }

        private void btnDocument_Click(object sender, EventArgs e)
        {
            if (!EnsureLayout())
            {
                return;
            }

            EditDimension("Document");
        }

        private void btnUI_Click(object sender, EventArgs e)
        {
            if (!EnsureLayout())
            {
                return;
            }

            EditDimension("UI");
        }

        private void btnEnum_Click(object sender, EventArgs e)
        {
            if (!EnsureLayout())
            {
                return;
            }

            EditDimension("Enum");
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Choose a html file");

            openHtmlFileDialog.InitialDirectory = @"C:\BigDoc\Design";

            if (openHtmlFileDialog.ShowDialog() == DialogResult.OK)
            {
                EnsureDocument(true);

                var fileName = openHtmlFileDialog.FileName;

                //import directory
                var dirName = fileName.Replace(".html", "_files");

                DirectoryInfo dirInfo = null;

                if (Directory.Exists(dirName))
                {
                    dirInfo = new DirectoryInfo(dirName);

                    FileHelpers.CopyFilesRecursively(dirName, FilesPath);
                }

                //import html
                var html = File.ReadAllText(fileName);

                html = HtmlHelpers.AddScriptsToHtml(html);

                if (dirInfo != null)
                {
                    html = HtmlHelpers.ReplaceLinks(html, $"./{dirInfo.Name}/", $"@BaseFilesUrl/{_collName}/");

                    html = Regex.Replace(html, "(?<Tag>src=\")(?<Path>[a-zA-Z0-9]+)", m =>
                    {
                        var path = m.Groups["Path"].Value;

                        if (path != "http" && path != "https")
                        {
                            return m.Groups["Tag"].Value + $"@BaseFilesUrl/{_collName}/" + path;
                        }
                        else
                        {
                            return m.Groups["Tag"].Value + path;
                        }
                    });

                    html = Regex.Replace(html, "(?<Tag>href=\")(?<Path>[a-zA-Z0-9]+)", m =>
                    {
                        var path = m.Groups["Path"].Value;

                        if (path != "http" && path != "https")
                        {
                            return m.Groups["Tag"].Value + $"@BaseFilesUrl/{_collName}/" + path;
                        }
                        else
                        {
                            return m.Groups["Tag"].Value + path;
                        }
                    });
                }

                if (!Directory.Exists(LayoutsPath))
                {
                    Directory.CreateDirectory(dirName);
                }

                SaveOptions(LayoutHtmlPath);

                var layoutDirName = new FileInfo(LayoutHtmlPath).DirectoryName;

                if(!Directory.Exists(layoutDirName))
                {
                    Directory.CreateDirectory(layoutDirName);
                }

                File.WriteAllText(LayoutHtmlPath, html);

                ChangeUILayout(_collName);

                Navigate(LayoutHtmlPath);
            }
        }

        private void btnRemoveLayout_Click(object sender, EventArgs e)
        {
            if (!EnsureLayout())
            {
                return;
            }

            if (!EnsureDocument())
            {
                return;
            }

            ChangeUILayout(null);

            if (File.Exists(tbLayout.Text))
            {
                File.Delete(tbLayout.Text);
            }

            var path = _documentFileName.Substring(0, _documentFileName.IndexOf("\\Databases"));

            var dirPath = $"{path}\\files\\{_dbName}\\{_collName}";

            if (Directory.Exists(dirPath))
            {
                Directory.Delete(dirPath, true);
            }

            tbLayout.Text = string.Empty;
        }

        private void btnDeployApp_Click(object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = _options.DeploymentToolPath,
                    WorkingDirectory = new FileInfo(_options.DeploymentToolPath).DirectoryName,
                    UseShellExecute = true,
                    RedirectStandardOutput = false,
                    RedirectStandardError = false,
                    CreateNoWindow = true
                }

            };

            process.Start();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (_currUndoUpdateIndex > 0)
            {
                var html = _undoUpdates[--_currUndoUpdateIndex];

                WriteHtml(html);

                RefreshPage();

                RefreshUndoRedoButtons();
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            if (_currUndoUpdateIndex < _undoUpdates.Count - 1)
            {
                var html = _undoUpdates[++_currUndoUpdateIndex];

                WriteHtml(html);

                RefreshPage();

                RefreshUndoRedoButtons();
            }
        }

        #endregion
    }
}