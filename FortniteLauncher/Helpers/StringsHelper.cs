using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FortniteLauncher.Helpers
{
    public class StringsHelper
    {
        public static string AddSpacesToNumbers(string input)
        {
            if (input.Contains("X")) //fix for season x
            {
                char findThis = 'X';
                int index = input.IndexOf(findThis);
                if (index >= 0 && index < input.Length - 1)
                {
                    return input.Insert(index - 1, " ");
                }
            }
            return Regex.Replace(input, @"(?<!\s)(\d+)(?!\s)", " $1 ");
        }
    }
}
