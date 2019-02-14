using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models.Data;
using AviaTicketsWpfApplication.Fundamentals;
using System.Collections.ObjectModel;
using AviaTicketsWpfApplication.Models;
using System;
using System.Text;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SearchQueryViewModel : ViewModelBase, IDataErrorInfo
    {
        private readonly ICacheService _cacheService;
		private readonly ISearchTicketsProvider _searchTicketsService;

		private readonly Lazy<Task<string>> _token;

        private SearchQuery _searchQuery = new SearchQuery();

        public RelayCommand SearchCommand { get; private set; }

        private bool _isSearchProgress;
        private bool IsSeachProgress
        {
            get { return _isSearchProgress; }
            set { Set(ref _isSearchProgress, value); }
        }

        private IList<City> _originalCities;
        public IList<City> OriginalCities 
        { 
            get
            {
                return _originalCities;
            }
            set
            {
                Set(ref _originalCities, value);
            }
        }

        private IList<City> _destinationCities;
        public IList<City> DestinationCities
        {
            get
            {
                return _destinationCities;
            }
            set
            {
                Set(ref _destinationCities, value);
            }
        }

        public City OriginalSelectValue
        {
            get
            {
                return _searchQuery.Original;
            }
            set
            {
                Set(ref _searchQuery.Original, value);
            }
        }

        public City DestinationSelectValue
        {
            get
            {
                return _searchQuery.Destination;
            }
            set
            {
                Set(ref _searchQuery.Destination, value);
            }
        }

        public DateTime? DepartDate
        {
            get
            {
                return _searchQuery.DepartDate;
            }
            set
            {
                Set(ref _searchQuery.DepartDate, value);
            }
        }

        public DateTime? ReturnDate
        {
            get
            {
                return _searchQuery.ReturnDate;
            }
            set
            {
                Set(ref _searchQuery.ReturnDate, value);
            }
        }

        public string Error
        {
            get;
            private set;
        }

        public string this[string columnName]
        {
            get
            {
                var bulderError = new StringBuilder();

                if (columnName == "OriginalSelectValue" && OriginalSelectValue == null)
                {
                    bulderError.AppendLine("Нужно указать город отправления.");
                }                
                else if (columnName == "DestinationSelectValue" && DestinationSelectValue == null)
                {
                    bulderError.AppendLine("Нужно указать город прибытия.");
                }
                else if (columnName == "DestinationSelectValue" && DestinationSelectValue == OriginalSelectValue)
                {
                    bulderError.AppendLine("Город прибытия не может совпадать с городом отправления");
                }
                else if (columnName == "DepartDate" && !DepartDate.HasValue)
                {
                    bulderError.AppendLine("Выбирите дату");
                }
                else if (columnName == "ReturnDate" && !ReturnDate.HasValue)
                {
                    bulderError.AppendLine("Выбирите дату");
                }

                Error = bulderError.ToString();
                SearchCommand.RaiseCanExecuteChanged();

                return Error;
            }
        }

        /// <summary>
        /// Initializes a new instance of the SearchQueryViewModel class.
        /// </summary>
        public SearchQueryViewModel(ICacheService cacheService, ISearchTicketsProvider searchTicketsService)
        {
            _cacheService = cacheService;
			_searchTicketsService = searchTicketsService;

			_token = new Lazy<Task<string>>(() => _cacheService.GetTokenAsync(), true);
			
			//_querySettings = new QuerySettings() { Currency = CurrencyType.USD };

            _isSearchProgress = false;

            SearchCommand = new RelayCommand(SearchCommandHandler, CanExecuteSearchCommand);
        }

        private bool CanExecuteSearchCommand()
        {
            return String.IsNullOrEmpty(Error) && !_isSearchProgress;
        }

        private async Task Load()
        {
            var listCities = (await _cacheService.GetAsync<IEnumerable<City>>(DataNames.Cities)).ToList();

            OriginalCities = listCities;
            DestinationCities = listCities;
        }

        private async void SearchCommandHandler()
        {
            IsSeachProgress = true;
            MessengerInstance.Send(new ViewModelMessage { IsShowingProgress = true });

			var token = await _token.Value;
            var tikets = await _searchTicketsService.GetPrice(token, _searchQuery.Original.Code, _searchQuery.Destination.Code, DepartDate.Value, ReturnDate.Value);

			MessengerInstance.Send(new ViewModelMessage { IsShowingProgress = false });
            IsSeachProgress = false;
        }
    }
}