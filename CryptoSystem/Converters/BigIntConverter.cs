using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CryptoSystem.Converters
{
    internal class BigIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            BigInteger result;
            string valueString = value.ToString();
            if (!BigInteger.TryParse(valueString, NumberStyles.Integer, null, out result))
            {
                var stripped = Regex.Replace(valueString, "\\D", "");
                return stripped.Length == 0 ? 0 : stripped;
            }
            return result;
        }
        
    }
}
