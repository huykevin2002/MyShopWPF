using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyShop.Utilities
{
    public class ThousandsSeparatorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int number1)
            {
                return FormatIntNumberWithThousandsSeparator(number1);
            }

            if (value is long number2)
            {
                return FormatLongNumberWithThousandsSeparator(number2);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string FormatIntNumberWithThousandsSeparator(int number)
        {
            return string.Format("{0:n0}", number);
        }

        private static string FormatLongNumberWithThousandsSeparator(long number)
        {
            return string.Format("{0:n0}", number);
        }
    }

}
