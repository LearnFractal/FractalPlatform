using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace FractalPlatform.CreateLayouts
{
    public partial class ChooseAttrForm : Form
    {
        private string _json;

        private string _parent;

        private Dictionary<string, string> _values;

        public string Attribute => cbAttribute.Text;

        public ChooseAttrForm(string json, string parent)
        {
            InitializeComponent();

            _json = json;

            _parent = parent;

            _values = new Dictionary<string, string>();
        }

        private void ChooseAttribute_Load(object sender, EventArgs e)
        {
            Init();
        }

        private void AddUniqueAttrs(List<string> tokens)
        {
            for (int len = 1; len <= tokens.Count; len++)
            {
                var subTokens = new List<string>();

                subTokens.AddRange(tokens);

                subTokens.RemoveRange(len, subTokens.Count - len);

                var attr = string.Join("\\", subTokens);

                if (!cbAttribute.Items.Contains(attr) && 
                    (string.IsNullOrEmpty(_parent) ||
                     attr.StartsWith(_parent) || //descending items
                     _parent.StartsWith(attr)))  //ascending items
                {
                    cbAttribute.Items.Add(attr);
                }
            }
        }

        private void Init()
        {
            bool hasValue = false;

            ushort level = 0;

            var isArray = new bool[256];

            var tokens = new List<string>();

            for (int i = 0; i < _json.Length;)
            {
                char c = _json[i];

                switch (c)
                {
                    case '/': //comment
                        {
                            if (_json[i + 1] == '/')
                            {
                                for (i += 2; i < _json.Length; i++)
                                {
                                    if (_json[i] == '\n')
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (_json[i + 1] == '*')
                            {
                                for (i += 2; i < _json.Length; i++)
                                {
                                    if (_json[i] == '*' &&
                                        _json[i + 1] == '/')
                                    {
                                        i++;

                                        break;
                                    }
                                }
                            }

                            break;
                        }
                    case '[':
                        {
                            isArray[++level] = true;

                            tokens.Add("[0]");

                            break;
                        }
                    case '{':
                        {
                            isArray[++level] = false;

                            break;
                        }
                    case '"':
                    case '\'':
                        {
                            var sb = new StringBuilder();

                            for (i++; _json[i] != c && _json[i] != c; i++)
                            {
                                if (_json[i] == '\\')
                                {
                                    i++;
                                }

                                sb.Append(_json[i]);
                            }

                            tokens.Add(sb.ToString());

                            hasValue = true;

                            break;
                        }
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        {
                            var sb = new StringBuilder();

                            for (; ('0' <= _json[i] && _json[i] <= '9') || _json[i] == '.'; i++)
                            {
                                sb.Append(_json[i]);
                            }

                            tokens.Add(sb.ToString());

                            hasValue = true;

                            continue;
                        }
                    case 't':
                    case 'f':
                    case 'n':
                        {
                            var sb = new StringBuilder();

                            uint len = (c == 't' || c == 'n' ? (uint)4 : (uint)5);

                            for (uint j = 0; j < len; i++, j++)
                            {
                                sb.Append(_json[i]);
                            }

                            tokens.Add(sb.ToString());

                            hasValue = true;

                            continue;
                        }
                    case ':':
                        {
                            break;
                        }
                    case '}':
                    case ']':
                    case ',': //find item
                        {
                            if (hasValue)
                            {
                                var value = tokens[tokens.Count - 1];

                                tokens.RemoveAt(tokens.Count - 1);

                                var attr = string.Join("\\", tokens);

                                if (!_values.ContainsKey(attr))
                                {
                                    _values.Add(attr, value);
                                }

                                AddUniqueAttrs(tokens);

                                hasValue = false;
                            }

                            if ((c != ',' || !isArray[level]) && tokens.Count > 0)
                            {
                                tokens.RemoveAt(tokens.Count - 1);
                            }

                            if (c == '}' || c == ']')
                            {
                                level--;
                            }

                            break;
                        }
                }

                i++;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbAttribute_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(_values.ContainsKey(cbAttribute.Text))
            {
                rtbValue.Text = _values[cbAttribute.Text];
            }
            else
            {
                rtbValue.Text = string.Empty;
            }
        }
    }
}
