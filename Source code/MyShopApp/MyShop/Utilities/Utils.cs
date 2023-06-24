using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyShop.Utilities
{
    public class Utils
    {
        public static bool IsNumericKey(Key key)
        {
            return key >= Key.D0 && key <= Key.D9 || key >= Key.NumPad0 && key <= Key.NumPad9;
        }

        public static bool IsTextAllowed(string text)
        {
            return Regex.IsMatch(text, "^[0-9]+$");
        }
    }
}
