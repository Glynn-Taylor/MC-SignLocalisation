using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCGT_SignTranslator.GTaylor.Data
{
    class TextUtil
    {
        public static string StripNonKeyCharacters(string s)
        {
            Regex rgx = new Regex("[^.a-zA-Z0-9 -]");
            return rgx.Replace(s, "");
        }
    }
}
