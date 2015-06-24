using System;
using System.Windows;
using System.Windows.Data;

namespace AviaTicketsWpfApplication.Converters
{
    [ValueConversion(typeof(string), typeof(object))]
    public sealed class StringToResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var res = Application.Current.Resources[value];

            return res;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
