using System;
using System.Collections.Generic;
using System.Text;

namespace ProfitAndLoss.Utilities.Helpers
{
    public static class StringHelpers
    {
        private const char hyphens = '-';
        private const char underscore = '_';
        private const char space = ' ';
        enum WordState
        {
            Start,
            Lower,
            Upper,
            NewWord,
            EndWord,
            Ignore
        }
        public static string HyphensCase(string s)
        {

            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            StringBuilder sb = new StringBuilder();
            WordState state = WordState.Start;
            bool checkUpper = false;
            foreach (var item in s)
            {
                checkUpper = char.IsUpper(item) ? true : false;
                if (!checkUpper) break;
            }
            if (!checkUpper)
            {
                s = s + "#";
                // to hypen
                for (int i = 0; i < s.Length - 1; i++)
                {
                    if (s[i] == underscore && i == 0)
                    {
                        state = WordState.Ignore;
                    }
                    else if (s[i] == underscore || s[i] == space)
                    {
                        if (sb[sb.Length - 1] != hyphens) sb.Append(hyphens);
                        state = WordState.Ignore;
                    }
                    else if (char.IsUpper(s[i]) && i != 0 && sb.Length > 0)
                    {
                        if (sb[sb.Length - 1] != hyphens) sb.Append(hyphens);
                        sb.Append(s[i]);
                        state = WordState.Ignore;
                    }
                    if (state != WordState.Ignore)
                    {
                        sb.Append(s[i]);
                    }
                    state = WordState.Lower;
                }
                // to lower
                sb.Replace("#", "");
            }
            else
            {
                return s.ToLower();
            }
            return sb.ToString().ToLower();
        }

        public static string ApprovalTransaction(this string member)
        {
            return $"Approval by {member} at {DateTime.Now.ToFormal()}";
        }
        public static string RejectTransaction(this string member)
        {
            return $"Reject by {member} at {DateTime.Now.ToFormal()}";
        }

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrEmpty(str?.Trim());
        }

        public static string ToFormal(this string str)
        {
            return str ?? "N/A";
        }
    }
}
