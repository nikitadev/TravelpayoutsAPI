using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AviaTicketsWpfApplication.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AviaTicketsWpfApplication.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AviaTicketsWpfApplication.Controls;assembly=AviaTicketsWpfApplication.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:MetroCalendar/>
    ///
    /// </summary>
    [TemplatePart(Name = "PART_Root", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_Title", Type = typeof(StackPanel))]
    [TemplatePart(Name = "PART_PreviousButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_NextButton", Type = typeof(Button))]
    [TemplatePart(Name = "PART_TitleHeader", Type = typeof(Label))]
    [TemplatePart(Name = "PART_TitleDayNames", Type = typeof(ItemsControl))]
    [TemplatePart(Name = "PART_Days", Type = typeof(ItemsControl))]
    public class MetroCalendar : Control
    {
        private Grid _gridRoot;
        private Label _labelTitle;
        private ItemsControl _titleWeekDayNames;
        private ItemsControl _days;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.Template != null)
            {
                var today = DateTime.Today;

                _gridRoot = GetTemplateChild("PART_Root") as Grid;
                _labelTitle = GetTemplateChild("PART_TitleHeader") as Label;
                _titleWeekDayNames = GetTemplateChild("PART_TitleDayNames") as ItemsControl;
                _days = GetTemplateChild("PART_Days") as ItemsControl;

                PreviousButtonElement = GetTemplateChild("PART_PreviousButton") as Button;
                NextButtonElement = GetTemplateChild("PART_NextButton") as Button;

                var dateFormat = GetCurrentDateFormat();
                _labelTitle.Content = String.Concat(dateFormat.GetMonthName(today.Month).ToUpper(), " ", today.Year);

                foreach (var name in dateFormat.DayNames)
                {
                    _titleWeekDayNames.Items.Add(name.ToUpper());
                }

                var day = new DateTime(today.Year, today.Month, 1);
                var lastDay = DateTime.DaysInMonth(today.Year, today.Month);
                for (int i = 1; i < lastDay; i++)
                {
                    _days.Items.Add(day.Day);

                    day = day.AddDays(1);
                }
            }
        }

        private Button _previousButtonElement;
        private Button PreviousButtonElement
        {
            get
            {
                return _previousButtonElement;
            }
            set
            {
                if (_previousButtonElement != null)
                {
                    _previousButtonElement.Click -=
                        new RoutedEventHandler(PreviousClick);
                }

                _previousButtonElement = value;

                if (_previousButtonElement != null)
                {
                    _previousButtonElement.Click +=
                        new RoutedEventHandler(PreviousClick);
                }
            }
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("PreviousClick");
        }

        private Button _nextButtonElement;
        private Button NextButtonElement
        {
            get
            {
                return _nextButtonElement;
            }
            set
            {
                if (_nextButtonElement != null)
                {
                    _nextButtonElement.Click -=
                        new RoutedEventHandler(NextClick);
                }

                _nextButtonElement = value;

                if (_nextButtonElement != null)
                {
                    _nextButtonElement.Click +=
                        new RoutedEventHandler(NextClick);
                }
            }
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("NextClick");
        }

        static MetroCalendar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroCalendar), new FrameworkPropertyMetadata(typeof(MetroCalendar)));
        }

        private DateTimeFormatInfo GetCurrentDateFormat()
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
