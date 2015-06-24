using System;
using System.Windows;
using System.Windows.Data;

namespace AviaTicketsWpfApplication.Converters
{
    [ValueConversion(typeof(Visibility), typeof(Boolean))]
    public sealed class BooleanToVisibilityInvertConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var booleanValue = (bool)value;

            return booleanValue ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
