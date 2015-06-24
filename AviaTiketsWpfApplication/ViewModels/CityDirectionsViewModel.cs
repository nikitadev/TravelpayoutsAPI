using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using AviaTicketsWpfApplication.ViewModels.Data;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
    public class CityDirectionsViewModel : BasePageChartViewModel<Tuple<string, double>>
    {
        private IEnumerable<City> _cities;

        /// <summary>
        /// Initializes a new instance of the CityDirectionsViewModel class.
        /// </summary>
        public CityDirectionsViewModel(ISearchTicketApiFactory searchTicketApiFactory, ICacheService cacheService)
            : base(searchTicketApiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            var message = new ViewModelMessage { IsSearhEnabled = true, IsShowedSearhPanel = true, IsShowingProgress = true };
            var flyoutMsg = new FlyoutMessage 
            { 
                TypeViewModel = typeof(SimpleSearchViewModel), 
                Header = String.Concat(Resources.Search, " ", Resources.Cities), 
                IsRightPosition = true 
            };

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                MessengerInstance.Send<FlyoutMessage>(flyoutMsg);
                MessengerInstance.Send<ViewModelMessage>(message);
            });

            _cities = await _cacheService.GetAsync<IEnumerable<City>>(DataNames.Cities);

            await base.InitializeAsync();

            message.IsShowingProgress = false;
            DispatcherHelper.CheckBeginInvokeOnUI(() => MessengerInstance.Send<ViewModelMessage>(message));
        }

        protected override async Task<IEnumerable<Tuple<string, double>>> UpdateCollection(ISearchQuery searchQuery)
        {
            var query = searchQuery as TextSearchQuery;
            if (query == null && !query.IsValidate)
            {
                SendError(Resources.ErrorParams);
                return null;
            }

            City city = null;
            string text = query.Text;
            if (text.Length == 3)
            {
                var cities = _cities.Where(a => a.Code != null && a.Code.Equals(text, StringComparison.OrdinalIgnoreCase));
                if (cities.Count() > 1)
                {
                    var cityList = String.Join("; ", cities.Select(a => a.CultureName));
                    SendError(Resources.FormatControlledDuplicates);

                    return null;
                }

                city = cities.FirstOrDefault();
            }

            if (city == null)
            {
                var cities = _cities.Where(a => (a.Name.ToLower().Contains(text.ToLower()) || a.CultureName.ToLower().Contains(text.ToLower())));
                if (cities.Count() > 1)
                {
                    MessengerInstance.Send<SearchResultMessage>(
                        new SearchResultMessage
                        {
                            Message = Resources.ChooseСity,
                            ListResult = cities.Select(a => new CityViewModel(a)).OfType<IHyperlinkViewModel>()
                        });

                    return null;
                }

                city = cities.SingleOrDefault();
            }            

            if (city == null)
            {
                SendError(Resources.CityNotFound);
                return null;
            }

            TitleChart = city.CultureName;
            string token = await _token.Value;
            try
            {
                var list = await _searchTicketApiFactory.PopularRoutes.GetPopularRoutesFromCity(token, city.Code);
                return list.Join(_cities, t => t.Destination, c => c.Code, (t, c) => new Tuple<string, double>(c.CultureName, t.Price));
            }
            catch (HttpRequestException ex)
            {
                SendError(Resources.Error404);
            }

            return null;
        }
    }
}