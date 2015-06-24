using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace AviaTicketsWpfApplication.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public sealed class DayShortNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTimeFormatInfo dateTimeFormat = GetCurrentDateFormat();
            string[] shortestDayNames = dateTimeFormat.ShortestDayNames;
            string[] dayNames = dateTimeFormat.DayNames;

            for (int i = 0; i < shortestDayNames.Count(); i++)
            {
                if (shortestDayNames[i] == value.ToString())
                {
                    return dayNames[i].ToUpper();
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        private static DateTimeFormatInfo GetCurrentDateFormat()
        {
            if (CultureInfo.CurrentCulture.Calendar is GregorianCalendar)
            {
                return CultureInfo.CurrentCulture.DateTimeFormat;
            }

            foreach (var cal in CultureInfo.CurrentCulture.OptionalCalendars)
            {
                if (cal is GregorianCalendar)
                {
                    var dtfi = new CultureInfo(CultureInfo.CurrentCulture.Name).DateTimeFormat;
                    dtfi.Calendar = cal;

                    return dtfi;
                }
            }

            var dt = new CultureInfo(CultureInfo.InvariantCulture.Name).DateTimeFormat;
            dt.Calendar = new GregorianCalendar();

            return dt;
        }
    }
}
