using CryptoSystem.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using XTR_TWOFISH.CypherEnums;

namespace CryptoSystem.Converters
{
    public class ModeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CypherMode mode)
            {
                
                if(mode == CypherMode.OFB)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c96d89"));
                }
                else if(mode == CypherMode.CBC || mode == CypherMode.CFB)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#acad51"));
                }
                else if (mode == CypherMode.CTR || mode == CypherMode.RD || mode == CypherMode.RDH)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2aad67"));
                }
                return Binding.DoNothing;
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
