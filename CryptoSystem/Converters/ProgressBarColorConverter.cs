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
    internal class ProgressBarColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double progress)
            {
                if(progress <= 50.0)
                {
                    return new SolidColorBrush(Color.FromRgb(255, (byte)(((progress * (255.0 / 50.0) > 255) ? 255 : progress * (255.0 / 50.0)) * 0.7), 0));
                }
                else
                {
                    return new SolidColorBrush(Color.FromRgb((byte)(255 - (byte)((progress * (255.0 / 50.0) - 255 > 255) ? 255 : progress * (255.0 / 50.0))), (byte)(255 * 0.7), 0));
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
