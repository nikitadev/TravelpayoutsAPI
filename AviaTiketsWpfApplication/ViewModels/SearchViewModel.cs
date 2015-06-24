using AviaTicketsWpfApplication.Fundamentals;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class SearchViewModel : BaseSearchViewModel<SearchQuery>
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

        public DateTime? DepartDate
        {
            get { return _searchQuery.DepartDate; }
            set { Set(ref _searchQuery.DepartDate, value); }
        }

        public DateTime? ReturnDate
        {
            get { return _searchQuery.ReturnDate; }
            set { Set(ref _searchQuery.ReturnDate, value); }
        }

        /// <summary>
        /// Initializes a new instance of the SearchViewModel class.
        /// </summary>
        public SearchViewModel(ISearchTicketApiFactory searchTicketApiFactory, ICacheService cacheService)
            : base(searchTicketApiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            base.InitializeAsync();

            _userLocationInfo = await _cacheService.GetAsync<UserLocationInfo>(CacheTags.USERINFO);
            var cities = await _cacheService.GetAsync<List<City>>(DataNames.Cities);

            OriginalCities = cities;
            DestinationCities = cities;

            OriginalSelectValue = cities.SingleOrDefault(c => c.Code == _userLocationInfo.Code);
        }

        public override bool GetValidate()
        {
            return base.GetValidate() && _searchQuery.IsValidate;
        }
    }
}