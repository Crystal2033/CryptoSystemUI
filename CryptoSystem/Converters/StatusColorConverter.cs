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
    public sealed class StatusColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Status status)
            {
                if(status == Status.RUNNING)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff0"));
                }
                else if(status == Status.SUCCESS)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0f0"));
                }
                else if (status == Status.FAILED)
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f00"));
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
