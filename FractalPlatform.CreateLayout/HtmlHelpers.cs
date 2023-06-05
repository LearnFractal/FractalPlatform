using System.Text;
using System.Text.RegularExpressions;

namespace FractalPlatform.CreateLayouts
{
    public static class HtmlHelpers
    {
        private static void AddSpaces(StringBuilder sb, int spaces)
        {
            for (int i = 0; i < spaces; i++)
            {
                sb.Append(' ');
            }
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
                                    sb.Append(html[i]);
                                    sb.AppendLine();

                                    sb.Append(@"<script src=""http://booben.com/jupiter/js/frmain.js""></script>");
                                }
                                else if (tagStr == "body" && !hasForm)
                                {
                                    sb.Append('<');
                                    sb.Append(tagStr);
                                    sb.Append(html[i]);
                                    sb.AppendLine();

                                    sb.Append(@"<form action=""/"" enctype=""multipart/form-data"" method=""post""> 
                                                <input type=""hidden"" name=""EditKey"" value="""" />
                                                <input type=""hidden"" name=""MenuAction"" value="""" />
                                                <input type=""submit"" id=""btnSaveOnEnter"" name=""btn"" value=""Save"" onclick=""return !isPreventSaveOnEnter;"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnSortGridColumn"" name=""btn"" value=""SortGridColumn"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnEditLanguage"" name=""btn"" value=""EditLanguage"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnEditTheme"" name=""btn"" value=""EditTheme"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnEventClick"" name=""btn"" value=""EventClick"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnEventChange"" name=""btn"" value=""EventChange"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnMenuClick"" name=""btn"" value=""MenuClick"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnSave"" name=""btn"" value=""Save"" style=""display:none""/>
                                                <input type=""submit"" id=""btnRefresh"" name=""btn"" value=""Refresh"" style=""display:none;"" />
                                                <input type=""submit"" id=""btnUpdate"" name=""btn"" value=""Update"" style=""display:none""/>
                                                <input type=""submit"" id=""btnCancel"" name=""btn"" value=""Cancel"" style=""display:none""/>
                                                ");
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
                                    sb.Append("</form>");
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

        //public static string ReplaceTagHtml(string tagId, string html, string newTag)
        //{
        //    var sb = new StringBuilder();

        //    for (int i = 0; i < html.Length; i++)
        //    {
        //        if (html[i] == '<')
        //        {
        //            var tag = new StringBuilder();

        //            if (html[i + 1] != '/')
        //            {
        //                for (++i; i < html.Length; i++)
        //                {
        //                    if (html[i] != '>')
        //                    {
        //                        tag.Append(html[i]);
        //                    }
        //                    else
        //                    {
        //                        var tagStr = tag.ToString();

        //                        //no Id attribute
        //                        var reg = new Regex($"[\\s]+[iI][dD][\\s]*=[\\s]*\"{tagId}\"");

        //                        if (!reg.IsMatch(tagStr))
        //                        {
        //                            sb.Append('<');
        //                            sb.Append(tagStr);
        //                        }
        //                        else
        //                        {
        //                            sb.Append(newTag);
        //                            i++;
        //                        }

        //                        break;
        //                    }
        //                }
        //            }
        //        }

        //        sb.Append(html[i]);
        //    }

        //    return sb.ToString();
        //}

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

                                //no Id attribute
                                var reg = new Regex($"[\\s]+[iI][dD][\\s]*=[\\s]*\"{tagId}\"");

                                if (reg.IsMatch(tagStr))
                                {
                                    start = currStart;

                                    ourTag = tagStr.Split(' ')[0];

                                    if(ourTag == "input" ||
                                       ourTag == "img") //tag that not closed
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
                                    return html.Substring(start, i - start + 1);
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
        
        public static string RemoveTagIds(string html)
        {
            return Regex.Replace(html, "\\sid=\"fr[0-9]+\"", string.Empty);
        }
    }
}
