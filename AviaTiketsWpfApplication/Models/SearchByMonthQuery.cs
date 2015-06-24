using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.ViewModels;
using System.Linq;
using System.Collections.Generic;
using System;

namespace AviaTicketsWpfApplication.Models
{
    public sealed class SearchByMonthQuery : BaseSearchQuery
    {
        public int DurationLower;
        public int DurationUpper;

        public readonly Dictionary<int, MonthViewModel> SelectedMonths;

        public SearchByMonthQuery()
        {
            SelectedMonths = new Dictionary<int, MonthViewModel>();
        }

        public int[] GetDurations()
        {
            if (DurationLower == DurationUpper)
                return new int[1] { DurationLower };

            return new int[2] { DurationLower, DurationUpper };
        }

        public string[] GetMonths()
        {
            if (SelectedMonths.Any())
            {
                var dates = new string[SelectedMonths.Keys.Count];

                int index = 0;
                foreach (int number in SelectedMonths.Keys)
                {
                    dates[index++] = String.Format("{0:yyyy}-{1:00}", DateTime.Today, number);
                }

                return dates;
            }

            return null;
        }

        public override bool IsValidate
        {
            get
            {
                bool isSelected = SelectedMonths.Any(m => m.Value != null && m.Value.IsChecked);
                return base.IsValidate && isSelected;
            }
        }
    }
}
