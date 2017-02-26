using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.ViewModels
{
    public sealed class RealtimeSearchViewModel : BaseSearchViewModel<RealtimeSearchQuery>, IDataErrorInfo
    {
        private readonly HashSet<string> _columnClearValidations;

        public bool IsEconomTripClass
        {
            get { return _searchQuery.TripClass.Equals("Y"); }
            set
            {
                _searchQuery.TripClass = value ? "Y" : "C";
                RaisePropertyChanged();
            }
        }

        public int? Adults
        {
            get { return _searchQuery.Adults; }
            set { Set(ref _searchQuery.Adults, value); }
        }

        public int? Children
        {
            get { return _searchQuery.Children; }
            set { Set(ref _searchQuery.Children, value); }
        }

        public int? Infants
        {
            get { return _searchQuery.Infants; }
            set { Set(ref _searchQuery.Infants, value); }
        }

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
            set { Set(ref _searchQuery.Original, value); }
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

        public string this[string columnName]
        {
            get
            {
                string result = String.Empty;
                if (columnName == nameof(Adults))
                {
                    if (!Adults.HasValue)
                    {
                        result = Resources.ValidateMsgAdults;
                    }
                }

                if (columnName == nameof(Infants))
                {
                    if (!Infants.HasValue)
                    {
                        result = Resources.ValidateMsgInfants;
                    }
                }

                if (columnName == nameof(Children))
                {
                    if (!Children.HasValue)
                    {
                        result = Resources.ValidateMsgChildren;
                    }
                }

                if (columnName == nameof(OriginalSelectValue))
                {
                    if (OriginalSelectValue == null)
                    {
                        result = Resources.ValidateMsgOrigin;
                    }
                }

                if (columnName == nameof(DestinationSelectValue))
                {
                    if (DestinationSelectValue == null)
                    {
                        result = Resources.ValidateMsgDestination;
                    }
                }

                if (columnName == nameof(DepartDate))
                {
                    if (!DepartDate.HasValue)
                    {
                        result = Resources.ValidateMsgDepartDate;
                    }
                }

                if (columnName == nameof(ReturnDate))
                {
                    if (!ReturnDate.HasValue)
                    {
                        result = Resources.ValidateMsgReturnDate;
                    }
                }

                if (!String.IsNullOrEmpty(result))
                {
                    _columnClearValidations.Add(columnName);
                }
                else if (_columnClearValidations.Contains(columnName))
                {
                    _columnClearValidations.Remove(columnName);
                }

                SearchCommand.RaiseCanExecuteChanged();

                return result;
            }
        }

        public string Error
        {
            get
            {
                return String.Join(", ", _columnClearValidations.ToArray());
            }
        }

        public RealtimeSearchViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
            _columnClearValidations = new HashSet<string>();
        }

        public override bool GetValidate()
        {
            return base.GetValidate() && String.IsNullOrEmpty(Error);
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            //_userLocationInfo = await _cacheService.GetAsync<UserLocationInfo>(CacheTags.USERINFO);
            var cities = await _cacheService.GetAsync<List<City>>(DataNames.Cities);

            OriginalCities = cities;
            DestinationCities = cities;

            //OriginalSelectValue = cities.SingleOrDefault(c => c.Code == _userLocationInfo.Code);
        }
    }
}