using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace AviaTicketsWpfApplication.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public sealed class AirlineCodeToImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            string code = value.ToString();
            if (String.IsNullOrEmpty(code))
                return null;

            string pathAirlineLogos = "pack://application:,,,/AviaTicketsWpfApplication;component/Resources/airline_logos/";
            string fullPathAirlineLogo = String.Concat(pathAirlineLogos, code, ".png");

            return fullPathAirlineLogo;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
