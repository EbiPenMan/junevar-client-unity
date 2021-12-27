using System;

namespace ProGraphGroup.General.Utility
{
    public sealed class TextUtils
    {
        public static bool isEmpty(String str)
        {
            if (string.IsNullOrEmpty(str))
                return true;

            if ("".Equals(str) || "null".Equals(str))
                return true;

            str = str.Trim();
            return "".Equals(str) || "null".Equals(str) || string.IsNullOrEmpty(str);
        }


        public static bool hasMentions(string text)
        {
            if (isEmpty(text))
            {
                return false;
            }

            return text.IndexOf('@', 0) != -1 || text.IndexOf('#', 0) != -1;
        }


        public static string getMentionText(in string text)
        {
            int commentLength = text.Length;

//		SpannableStringBuilder stringBuilder = new SpannableStringBuilder(text, 0, commentLength);

            for (int i = 0; i < commentLength; ++i)
            {
                char currChar = text[i];

                if (currChar == '@' || currChar == '#')
                {
                    int startLen = i;

                    do
                    {
                        if (++i == commentLength)
                        {
                            break;
                        }

                        currChar = text[i];

                        if (currChar == '.' && i + 1 < commentLength)
                        {
                            char nextChar = text[i + 1];
                            if (nextChar == '.' || nextChar == ' ' || nextChar == '#' || nextChar == '@' ||
                                nextChar == '/' || nextChar == '\r' || nextChar == '\n')
                            {
                                break;
                            }
                        }
                        else if (currChar == '.')
                        {
                            break;
                        }

                        // for merged hashtags
                        if (currChar == '#')
                        {
                            --i;
                            break;
                        }
                    } while (currChar != ' ' && currChar != '\r' && currChar != '\n' && currChar != '>' &&
                             currChar != '<' && currChar != ':' && currChar != ';' && currChar != '\'' &&
                             currChar != '"' && currChar != '[' && currChar != ']' && currChar != '\\' &&
                             currChar != '=' && currChar != '-' && currChar != '!' && currChar != '$' &&
                             currChar != '%' && currChar != '^' && currChar != '&' && currChar != '*' &&
                             currChar != '(' && currChar != ')' && currChar != '{' && currChar != '}' &&
                             currChar != '/' && currChar != '|' && currChar != '?' && currChar != '`' &&
                             currChar != '~');


                    int endLen = currChar != '#' ? i : i + 1; // for merged hashtags
//				stringBuilder.setSpan(new CommentMentionClickSpan(), startLen, Math.Min(commentLength, endLen), Spanned.SPAN_EXCLUSIVE_INCLUSIVE);
                }
            }

//		return stringBuilder;
            return text;
        }
    }
}