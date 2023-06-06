using System;
using System.Text;

namespace FractalPlatform.CreateLayouts
{
    internal static class JsonHelpers
    {
        private static void AddSpaces(StringBuilder sb,
                                      int indent)
        {
            for (int i = 0; i < indent; i++)
            {
                sb.Append(' ');
            }
        }

        private static void RaiseError(string message, string json, int position = 0)
        {
            if (position > 0)
            {
                json = json.Insert(position, "[ERROR NEAR HERE]");

                throw new InvalidOperationException($"{message}. Position: {position}.\r\nJson:" + json);
            }
            else
            {
                throw new InvalidOperationException($"{message}. Json:" + json);
            }
        }

        public static void ValidateJson(string json,
                                        Action<string, string, int> handleError = null)
        {
            if (handleError == null)
            {
                handleError = RaiseError;
            }

            if (!json.StartsWith("{"))
            {
                handleError("Json should start from '{' bracket.\r\nJson: ", json, 0);

                return;
            }

            var brackets = new StringBuilder();

            var isArray = false;

            var waitAttribute = false;

            var waitValue = true;

            var waitComma = false;

            var waitFunc = false;

            var prevChar = '\0';

            for (int i = 0; i < json.Length; i++)
            {
                var c = json[i];

                var isSkipChar = false;

                switch (c)
                {
                    case '{':
                        {
                            if (waitComma && brackets.Length > 0)
                            {
                                handleError($"Comma is not found", json, i);

                                return;
                            }

                            if (waitFunc && brackets.Length > 0)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            isArray = false;

                            waitAttribute = true;

                            if (!waitValue && brackets.Length > 0)
                            {
                                handleError($"Value is not found", json, i);

                                return;
                            }

                            waitValue = false;

                            brackets.Append(c);

                            break;
                        }
                    case ',':
                        {
                            if (!waitComma)
                            {
                                handleError($"Attribute is not found", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (isArray)
                            {
                                waitAttribute = false;

                                waitValue = true;
                            }
                            else
                            {
                                waitAttribute = true;

                                waitValue = false;
                            }

                            waitComma = false;

                            break;
                        }
                    case ':':
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            waitAttribute = false;

                            waitValue = true;

                            waitComma = false;

                            break;
                        }
                    case '}':
                        {
                            if (isArray)
                            {
                                handleError("Brackets '{' and '}' don't match to each other", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if ((waitAttribute || waitValue) &&
                                prevChar != '{' &&
                                prevChar != '/')
                            {
                                handleError($"Attribute or value is not found, position", json, i);

                                return;
                            }

                            brackets.Remove(brackets.Length - 1, 1);

                            if (brackets.Length > 0)
                            {
                                var lastChar = brackets[brackets.Length - 1];

                                if (lastChar == '{')
                                {
                                    isArray = false;
                                }
                                else if (lastChar == '[')
                                {
                                    isArray = true;
                                }
                            }

                            waitComma = true;

                            waitAttribute = false;

                            waitValue = false;

                            break;
                        }
                    case '[':
                        {
                            if (!waitValue)
                            {
                                handleError($"Value is not found", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            brackets.Append(c);

                            isArray = true;

                            waitAttribute = false;

                            waitValue = true;

                            waitComma = false;

                            break;
                        }
                    case ']':
                        {
                            if (!isArray)
                            {
                                handleError("Brackets '[' and ']' don't match to each other", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if ((waitAttribute || waitValue) &&
                                prevChar != '[')
                            {
                                handleError($"Attribute or value is not found, position", json, i);

                                return;
                            }

                            brackets.Remove(brackets.Length - 1, 1);

                            var lastChar = brackets[brackets.Length - 1];

                            if (lastChar == '{')
                            {
                                isArray = false;
                            }
                            else if (lastChar == '[')
                            {
                                isArray = true;
                            }

                            waitComma = true;

                            waitAttribute = false;

                            waitValue = false;

                            break;
                        }
                    case 't': //true
                        {
                            if (waitAttribute || !waitValue)
                            {
                                handleError($"Value is not found", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (json[i + 1] != 'r' ||
                                json[i + 2] != 'u' ||
                                json[i + 3] != 'e')
                            {
                                handleError($"'True' token is not recognized", json, i);

                                return;
                            }

                            i += 3;

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case 'f': //false
                        {
                            if (waitAttribute || !waitValue)
                            {
                                handleError($"Value is not found", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (json[i + 1] != 'a' ||
                                json[i + 2] != 'l' ||
                                json[i + 3] != 's' ||
                                json[i + 4] != 'e')
                            {
                                handleError($"'False' token is not recognized", json, i);

                                return;
                            }

                            i += 4;

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case 'n': //null
                        {
                            if (waitAttribute || !waitValue)
                            {
                                handleError($"Value is not found", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (json[i + 1] != 'u' ||
                                json[i + 2] != 'l' ||
                                json[i + 3] != 'l')
                            {
                                handleError($"'null' token is not recognized", json, i);

                                return;
                            }

                            i += 3;

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case '$':
                        {
                            if (waitAttribute || !waitValue)
                            {
                                handleError($"Value is not found", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case '#': //#DocID
                        {
                            if (waitAttribute || !waitValue)
                            {
                                handleError($"Value is not found", json, i);

                                return;
                            }

                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (json[i + 1] != 'D' ||
                                json[i + 2] != 'o' ||
                                json[i + 3] != 'c' ||
                                json[i + 4] != 'I' ||
                                json[i + 5] != 'D')
                            {
                                handleError($"'#DocID' token is not recognized", json, i);

                                return;
                            }

                            i += 5;

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case 'R': //Repeat, Range
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (json[i + 1] == 'a' &&
                                json[i + 2] == 'n' &&
                                json[i + 3] == 'g' &&
                                json[i + 4] == 'e')
                            {
                                if (isArray)
                                {
                                    handleError($"'Range' token in an array", json, i);

                                    return;
                                }

                                i += 4;

                                waitFunc = true;
                            }
                            else
                            {
                                if (!isArray)
                                {
                                    handleError($"'R' token in not an array", json, i);

                                    return;
                                }
                            }

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case 'P': //Portion
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (!isArray)
                            {
                                handleError($"'P' token in not an array", json, i);

                                return;
                            }

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case 'A': //Add, Any
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if ((json[i + 1] == 'd' &&
                                 json[i + 2] == 'd'))
                            {
                                i += 2;

                                waitValue = false;

                                if (isArray)
                                {
                                    waitComma = true;
                                }
                                else
                                {
                                    waitFunc = true;
                                }
                            }
                            else if (json[i + 1] == 'n' &&
                                    json[i + 2] == 'y')
                            {
                                i += 2;

                                waitValue = false;

                                if (isArray)
                                {
                                    waitComma = true;
                                }
                                else
                                {
                                    waitFunc = true;
                                }
                            }
                            else
                            {
                                handleError($"'Add' or 'Any' token is not recognized", json, i);

                                return;
                            }

                            break;
                        }
                    case 'S': //Sub
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if ((json[i + 1] == 'u' &&
                                 json[i + 2] == 'b'))
                            {
                                i += 2;

                                if (isArray)
                                {
                                    handleError($"'Sub' token in an array", json, i);

                                    return;
                                }

                                waitValue = false;

                                waitFunc = true;
                            }
                            else
                            {
                                handleError($"'Sub' token is not recognized", json, i);

                                return;
                            }

                            break;
                        }
                    case 'I': //In
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (!isArray)
                            {
                                handleError($"'In' token in not array", json, i);

                                return;
                            }

                            if (json[i + 1] != 'n')
                            {
                                handleError($"'In' token is not recognized", json, i);

                                return;
                            }

                            i++;

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case 'N': //Nin, Not
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (json[i + 1] == 'i' &&
                                json[i + 2] == 'n')
                            {
                                if (!isArray)
                                {
                                    handleError($"'Nin' token in not an array", json, i);

                                    return;
                                }

                                waitValue = false;

                                waitComma = true;

                                i += 2;
                            }
                            else if (json[i + 1] == 'o' &&
                                     json[i + 2] == 't')
                            {
                                if (isArray)
                                {
                                    handleError($"'Not' token in an array", json, i);

                                    return;
                                }

                                waitValue = false;

                                waitFunc = true;

                                i += 2;
                            }
                            else
                            {

                                handleError($"'Nin' token is not recognized", json, i);

                                return;
                            }

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
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (waitAttribute || !waitValue)
                            {
                                handleError($"Attribute name is not found", json, i);

                                return;
                            }

                            int j = 0;

                            for (; i < json.Length - 1; i++, j++)
                            {
                                if (!char.IsNumber(json[i + 1]) && json[i + 1] != '.')
                                {
                                    break;
                                }

                                if (j > 20) //max len of number is 20 symbols
                                {
                                    handleError("Number value is too long", json, i);

                                    return;
                                }
                            }

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case '"':
                    case '\'':
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (!waitAttribute && !waitValue)
                            {
                                handleError($"Attribute name or value is not found", json, i);

                                return;
                            }

                            int j = 0;

                            for (++i; i < json.Length; i++, j++)
                            {
                                if (json[i] == '\\')
                                {
                                    i++;

                                    continue;
                                }

                                if (json[i] == c)
                                {
                                    break;
                                }

                                if (j > 940) //max len of key+value = ~940
                                {
                                    handleError("String value is too long", json, i);

                                    return;
                                }
                            }

                            if (waitValue)
                            {
                                waitComma = true;
                            }
                            else
                            {
                                waitComma = false;
                            }

                            waitAttribute = false;

                            waitValue = false;

                            break;
                        }
                    case '/': //skip comment
                        {
                            if (json[i + 1] == '/')
                            {
                                for (i += 2; i < json.Length; i++)
                                {
                                    if (json[i] == '\r' ||
                                        json[i] == '\n')
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                handleError($"Attribute name or value is not found", json, i);
                            }

                            break;
                        }
                    case 'L': //Less, LessOrEqual, Last
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (waitAttribute || !waitValue)
                            {
                                handleError($"'Less' or 'LessOrEqual' token is not found", json, i);

                                return;
                            }

                            if (json[i + 1] == 'e' &&
                                json[i + 2] == 's' &&
                                json[i + 3] == 's')
                            {
                                if (json[i + 4] == 'O' &&
                                    json[i + 5] == 'r' &&
                                    json[i + 6] == 'E' &&
                                    json[i + 7] == 'q' &&
                                    json[i + 8] == 'u' &&
                                    json[i + 9] == 'a' &&
                                    json[i + 10] == 'l')
                                {
                                    i += 10;
                                }
                                else
                                {
                                    i += 3;
                                }

                                waitValue = false;

                                waitComma = false;

                                waitFunc = true;
                            }
                            else if (json[i + 1] == 'a' &&
                                     json[i + 2] == 's' &&
                                     json[i + 3] == 't')
                            {
                                if (!isArray)
                                {
                                    handleError($"'Nin' token in not an array", json, i);

                                    return;
                                }

                                i += 3;

                                waitValue = false;

                                waitComma = true;
                            }
                            else
                            {
                                handleError($"'Less' or 'LessOrEqual' token is not recognized", json, i);

                                return;
                            }

                            break;
                        }
                    case 'G': //Great, GreatOrEqual
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (waitAttribute || !waitValue)
                            {
                                handleError($"'Great' or 'GreatOrEqual' token is not found", json, i);

                                return;
                            }

                            if (json[i + 1] == 'r' &&
                                json[i + 2] == 'e' &&
                                json[i + 3] == 'a' &&
                                json[i + 4] == 't')
                            {
                                if (json[i + 5] == 'O' &&
                                    json[i + 6] == 'r' &&
                                    json[i + 7] == 'E' &&
                                    json[i + 8] == 'q' &&
                                    json[i + 9] == 'u' &&
                                    json[i + 10] == 'a' &&
                                    json[i + 11] == 'l')
                                {
                                    i += 11;
                                }
                                else
                                {
                                    i += 4;
                                }
                            }
                            else
                            {
                                handleError($"'Great' or 'GreatOrEqual' token is not recognized", json, i);

                                return;
                            }

                            waitValue = false;

                            waitComma = false;

                            waitFunc = true;

                            break;
                        }
                    case 'T': //Template
                        {
                            if (waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            if (waitAttribute || !waitValue)
                            {
                                handleError($"'Template' token is not found", json, i);

                                return;
                            }

                            if (json[i + 1] != 'e' ||
                                json[i + 2] != 'm' ||
                                json[i + 3] != 'p' ||
                                json[i + 4] != 'l' ||
                                json[i + 5] != 'a' ||
                                json[i + 6] != 't' ||
                                json[i + 7] != 'e')
                            {
                                handleError($"'Template' token is not recognized", json, i);

                                return;
                            }

                            i += 7;

                            waitValue = false;

                            waitComma = false;

                            waitFunc = true;

                            break;
                        }
                    case '@':
                        {
                            for (; i < json.Length - 1; i++)
                            {
                                if (!char.IsNumber(json[i + 1]) &&
                                    !char.IsLetter(json[i + 1]))
                                {
                                    break;
                                }
                            }

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case '(':
                        {
                            if (!waitFunc)
                            {
                                handleError($"Func is not found", json, i);

                                return;
                            }

                            for (++i; i < json.Length; i++)
                            {
                                if (json[i] == '\\')
                                {
                                    i++;

                                    continue;
                                }

                                if (json[i] == ')')
                                {
                                    break;
                                }
                            }

                            waitFunc = false;

                            waitValue = false;

                            waitComma = true;

                            break;
                        }
                    case ' ':
                    case '\t':
                    case '\n':
                    case '\r':
                        {
                            isSkipChar = true;

                            break;
                        }
                    case '!':
                        {
                            break;
                        }
                    default:
                        {
                            handleError("Attribute should be started from quote.", json, i);

                            return;
                        }
                };

                if (!isSkipChar)
                {
                    prevChar = c;
                }
            }

            if (brackets.Length != 0)
            {
                handleError("Brackets don't match to each other", json, 0);

                return;
            }
        }

        public static string FormatJson(string json)
        {
            StringBuilder sb = new StringBuilder();

            int indent = 0;

            for (int i = 0; i < json.Length; i++)
            {
                char c = json[i];

                if (c == '\n' ||
                    c == '\r' ||
                    c == '\t' ||
                    c == ' ')
                {
                    continue;
                }

                if (c == '"' || c == '\'')
                {
                    sb.Append(c);

                    for (i++; i < json.Length; i++)
                    {
                        sb.Append(json[i]);

                        if (json[i] == '\\')
                        {
                            sb.Append(json[++i]);

                            continue;
                        }

                        if (json[i] == '"' || json[i] == '\'')
                            break;
                    }
                }
                else
                {
                    if (c == '}' || c == ']')
                    {
                        indent -= 3;
                        sb.Append("\r\n");
                        AddSpaces(sb, indent);
                    }

                    sb.Append(c);

                    if (c == '{' || c == '[')
                    {
                        indent += 3;
                        sb.Append("\r\n");
                        AddSpaces(sb, indent);
                    }
                    else if (c == ',')
                    {
                        sb.Append("\r\n");
                        AddSpaces(sb, indent);
                    }
                    else if(c == ':')
                    {
                        AddSpaces(sb, 1);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
