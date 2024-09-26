using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FractalPlatform.CreateLayouts
{
    public static class HtmlHelpers
    {
        private readonly static List<string> _emptyTags = new List<string> { "input", "img", "br" };

        private static void AddSpaces(StringBuilder sb, int spaces)
        {
            for (int i = 0; i < spaces; i++)
            {
                sb.Append(' ');
            }
        }

        private static string GetTagAttrs(string html, ref int i)
        {
            var str = string.Empty;

            while (true)
            {
                str += html[i];

                if (html[i] == '>')
                    break;

                i++;
            }

            str = Regex.Replace(str, "id=\"fr[0-9]+\"", string.Empty);

            return str;
        }

        public static string AddScriptsToHtml(string html)
        {
            var hasScript = html.Contains("frmain.js");
            var hasForm = html.Contains("btnSaveOnEnter");

            var sb = new StringBuilder();

            for (int i = 0; i < html.Length; i++)
            {
                if (html[i] == '<' && html[i + 1] != '!')
                {
                    var tag = new StringBuilder();

                    if (html[i + 1] != '/')
                    {
                        for (++i; i < html.Length; i++)
                        {
                            if (char.IsLetter(html[i]))
                            {
                                tag.Append(html[i]);
                            }
                            else
                            {
                                var tagStr = tag.ToString();

                                if (tagStr == "form") //remove form tag
                                {
                                    for (; i < html.Length; i++)
                                    {
                                        if (html[i] == '>')
                                            break;
                                    }
                                }
                                else if (tagStr == "head" && !hasScript)
                                {
                                    sb.Append('<');
                                    sb.Append(tagStr);
                                    sb.Append(GetTagAttrs(html, ref i));
                                    
                                    sb.AppendLine();

                                    sb.Append(@"@Styles");

                                }
                                else if (tagStr == "body" && !hasForm)
                                {
                                    sb.Append('<');
                                    sb.Append(tagStr);
                                    sb.Append(GetTagAttrs(html, ref i));

                                    sb.AppendLine();

                                    sb.Append(@"@StartForm");
                                }
                                else  //not layout location
                                {
                                    sb.Append('<');
                                    sb.Append(tagStr);
                                    sb.Append(html[i]);
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        for (i += 2; i < html.Length; i++)
                        {
                            if (char.IsLetter(html[i]))
                            {
                                tag.Append(html[i]);
                            }
                            else
                            {
                                var tagStr = tag.ToString();

                                if (tagStr == "form")
                                {
                                    for (; i < html.Length; i++)
                                    {
                                        tag.Append(html[i]);

                                        if (html[i] == '>')
                                            break;
                                    }
								}
                                else if (tagStr == "body" && !hasForm)
                                {
                                    sb.Append("@EndForm");
                                    sb.Append('<');
                                    sb.Append('/');
                                    sb.Append(tagStr);
                                    sb.Append(html[i]);
                                }
                                else  //not control
                                {
                                    sb.Append('<');
                                    sb.Append('/');
                                    sb.Append(tagStr);
                                    sb.Append(html[i]);
                                }

                                break;
                            }
                        }
                    }
                }
                else
                {
                    sb.Append(html[i]);
                }
            }

            return sb.ToString();
        }

        public static string ReplaceLinks(string html, string oldLink, string newLink)
        {
            return html.Replace(oldLink, newLink);
        }

        //private static int GetLastTagId(string html)
        //{
        //    var reg = new Regex("\"fr(?<Id>[0-9]+)\"");

        //    var lastID = 0;

        //    foreach (Match match in reg.Matches(html))
        //    {
        //        var id = int.Parse(match.Groups["Id"].Value);

        //        if (id > lastID)
        //        {
        //            lastID = id;
        //        }
        //    }

        //    return lastID;
        //}

        public static string AddTagIdsToHtml(string html)
        {
            var lastID = 0;

            var sb = new StringBuilder();

            for (int i = 0; i < html.Length; i++)
            {
                if (html[i] == '<' && html[i + 1] != '!')
                {
                    var tag = new StringBuilder();

                    if (html[i + 1] != '/')
                    {
                        for (++i; i < html.Length; i++)
                        {
                            if (html[i] == '>' || (html[i] == '/' && html[i + 1] == '>'))
                            {
                                var tagStr = tag.ToString();

                                sb.Append('<');
                                sb.Append(tagStr);

                                //no Id attribute
                                var reg = new Regex("[\\s]+[iI][dD][\\s]*=");

                                if (!reg.IsMatch(tagStr))
                                {
                                    sb.Append($" id=\"fr{++lastID}\"");
                                }

                                if (html[i] == '/')
                                {
                                    sb.Append(html[i]);

                                    i++;
                                }

                                break;
                            }
                            else
                            {
                                tag.Append(html[i]);
                            }
                        }
                    }
                }

                sb.Append(html[i]);
            }

            return sb.ToString();
        }

        public static string ReplaceTagHtml(string html, string tagId, string tagHtml)
        {
            var currStart = 0;

            var currHtml = GetTagHtml(html, tagId, ref currStart);

            return html.Remove(currStart, currHtml.Length)
                       .Insert(currStart, tagHtml);
        }

        public static string GetTagHtml(string html, string tagId)
        {
            var currStart = 0;

            return GetTagHtml(html, tagId, ref currStart);
        }

        public static string GetTagHtml(string html, string tagId, ref int start)
        {
            string ourTag = null;

            var level = 0;

            for (int i = 0; i < html.Length; i++)
            {
                if (html[i] == '<' && html[i + 1] != '!')
                {
                    var currStart = i;

                    var tag = new StringBuilder();

                    if (html[i + 1] != '/')
                    {
                        for (++i; i < html.Length; i++)
                        {
                            if (html[i] != '>')
                            {
                                tag.Append(html[i]);
                            }
                            else
                            {
                                var tagStr = tag.ToString();

                                var currTag = tagStr.Split(' ')[0];

                                if (currTag == ourTag)
                                {
                                    level++;
                                }

                                //no Id attribute
                                var reg = new Regex($"[\\s]+[iI][dD][\\s]*=[\\s]*\"{tagId}\"");

                                if (reg.IsMatch(tagStr))
                                {
                                    start = currStart;

                                    ourTag = currTag;

                                    if (_emptyTags.Contains(ourTag))
                                    {
                                        return "<" + tagStr + html[i];
                                    }
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        for (i += 2; i < html.Length; i++)
                        {
                            if (char.IsLetter(html[i]) || char.IsNumber(html[i]))
                            {
                                tag.Append(html[i]);
                            }
                            else
                            {
                                var tagStr = tag.ToString();

                                if (tagStr == ourTag)
                                {
                                    if (level == 0)
                                    {
                                        return html.Substring(start, i - start + 1);
                                    }
                                    else
                                    {
                                        level--;
                                    }
                                }

                                break;
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static string FormatHtml(string html)
        {
            if (string.IsNullOrEmpty(html))
                return html;

            var sb = new StringBuilder();

            var spaces = 0;

            for (int i = 0; i < html.Length; i++)
            {
                if (html[i] == '<' && html[i + 1] != '!')
                {
                    var tag = new StringBuilder();

                    if (html[i + 1] != '/')
                    {
                        for (++i; i < html.Length; i++)
                        {
                            if (html[i] != '>')
                            {
                                tag.Append(html[i]);
                            }
                            else
                            {
                                var tagStr = tag.ToString();

                                sb.Append('\n');
                                AddSpaces(sb, spaces);

                                sb.Append('<');
                                sb.Append(tagStr);

                                var currTag = tagStr.Split(' ')[0];

                                if (!_emptyTags.Contains(currTag))
                                {
                                    spaces += 3;
                                }

                                break;
                            }
                        }
                    }
                    else
                    {
                        for (i += 2; i < html.Length; i++)
                        {
                            if (char.IsLetter(html[i]) || char.IsNumber(html[i]))
                            {
                                tag.Append(html[i]);
                            }
                            else
                            {
                                var tagStr = tag.ToString();

                                spaces -= 3;

                                sb.Append('\n');
                                AddSpaces(sb, spaces);

                                sb.Append("</");
                                sb.Append(tagStr);

                                break;
                            }
                        }
                    }
                }

                sb.Append(html[i]);

                if (html[i] == '>')
                {
                    while (i < html.Length - 1 &&
                           (html[i + 1] == ' ' ||
                            html[i + 1] == '\n' ||
                            html[i + 1] == '\t'))
                    {
                        i++;
                    }

                    sb.Append('\n');

                    AddSpaces(sb, spaces);
                }
                else if (html[i] == '\n')
                {
                    AddSpaces(sb, spaces);
                }
            }

            return sb.ToString().Trim();
        }

        public static string RemoveTagIds(string html)
        {
            return Regex.Replace(html, "\\sid=\"fr[0-9]+\"", string.Empty);
        }
    }
}
