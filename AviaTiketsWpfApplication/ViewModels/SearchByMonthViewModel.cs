using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SearchByMonthViewModel : BaseSearchViewModel<SearchByMonthQuery>
    {
        private UserLocationInfo _userLocationInfo;

        private List<City> _originalCities;
        public List<City> OriginalCities
        {
            get { return _originalCities; }
            set { Set(ref _originalCities, value); }
        }

        private List<City> _destinationCities;
        public List<City> DestinationCities
        {
            get { return _destinationCities; }
            set { Set(ref _destinationCities, value); }
        }

        public City OriginalSelectValue
        {
            get { return _searchQuery.Original; }
            set
            {
                Set(ref _searchQuery.Original, value);
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        public City DestinationSelectValue
        {
            get { return _searchQuery.Destination; }
            set { Set(ref _searchQuery.Destination, value); }
        }

        private List<MonthViewModel> _months;
        public List<MonthViewModel> Months
        {
            get { return _months; }
            set { Set(ref _months, value); }
        }

        public int DurationLower
        {
            get { return _searchQuery.DurationLower; }
            set { Set(ref _searchQuery.DurationLower, value); }
        }

        public int DurationUpper
        {
            get { return _searchQuery.DurationUpper; }
            set { Set(ref _searchQuery.DurationUpper, value); }
        }

        /// <summary>
        /// Initializes a new instance of the SearchByMonthViewModel class.
        /// </summary>
        public SearchByMonthViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
            DurationLower = 7;
            DurationUpper = 14;
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            _userLocationInfo = await _cacheService.GetAsync<UserLocationInfo>(CacheTags.USERINFO);
            var cities = await _cacheService.GetAsync<List<City>>(DataNames.Cities);

            OriginalCities = cities;
            DestinationCities = cities;

            OriginalSelectValue = cities.SingleOrDefault(c => c.Code == _userLocationInfo.Code);

            var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;

            int index = 1;
            var months = dateTimeFormat.MonthNames
                .Select(m => new MonthViewModel(index++, UpdateSelectedMonth) { Name = m })
                .ToList();

            Months = new List<MonthViewModel>(months.SkipWhile(m => m.Number != DateTime.Today.Month).Where(m => !String.IsNullOrEmpty(m.Name)));
        }

        public override bool GetValidate()
        {
            return base.GetValidate() && _searchQuery.IsValidate;
        }

        private void UpdateSelectedMonth(MonthViewModel monthViewModel)
        {
            MonthViewModel viewModel;
            bool exist = _searchQuery.SelectedMonths.TryGetValue(monthViewModel.Number, out viewModel);

            if (monthViewModel.IsChecked && !exist)
            {
                _searchQuery.SelectedMonths.Add(monthViewModel.Number, monthViewModel);
            }
            else if (exist)
            {
                _searchQuery.SelectedMonths.Remove(viewModel.Number);
            }

            SearchCommand.RaiseCanExecuteChanged();
        }
    }
}