using CryptoSystem.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CryptoSystem.Converters
{
    public sealed class KeySizeColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int keySize)
            {
                if (keySize == 128)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#c96d89"));
                }
                else if (keySize == 192)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#acad51"));
                }
                else if (keySize == 256)
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
