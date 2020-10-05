using Newtonsoft.Json.Serialization;
using Pluralize.NET;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi.Helpers
{
    public class HypensNamingStrategy : NamingStrategy
    {
        private const char hyphens = '-';
        private const char underscore = '_';
        private const char space = ' ';
        
        public HypensNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames)
        {
            ProcessDictionaryKeys = processDictionaryKeys;
            OverrideSpecifiedNames = overrideSpecifiedNames;
        }

        public HypensNamingStrategy(bool processDictionaryKeys, bool overrideSpecifiedNames, bool processExtensionDataNames)
            : this(processDictionaryKeys, overrideSpecifiedNames)
        {
            ProcessExtensionDataNames = processExtensionDataNames;
        }

        public HypensNamingStrategy()
        {
        }

        protected override string ResolvePropertyName(string name)
        {
            return HyphensCase(name);
        }

        enum WordState
        {
            Start,
            Lower,
            Upper,
            NewWord,
            EndWord,
            Ignore
        }
        static string Pluralize(string s)
        {
            Pluralizer pluralizationService = new Pluralizer();
            return pluralizationService.Pluralize(s);
        }
        static string HyphensCase(string s)
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
                    else if (char.IsUpper(s[i]) && i != 0 && sb.Length >0)
                    {
                        if (sb[sb.Length-1] != hyphens) sb.Append(hyphens);
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
    }
}
