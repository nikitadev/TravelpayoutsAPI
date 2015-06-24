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
    public class AirlineDirectionsViewModel : BasePageChartViewModel<Tuple<string, int>>
    {
        private IEnumerable<City> _cities;
        private IEnumerable<Airline> _airlines;

        /// <summary>
        /// Initializes a new instance of the AirlineDirectionsViewModel class.
        /// </summary>
        public AirlineDirectionsViewModel(ISearchTicketApiFactory searchTicketApiFactory, ICacheService cacheService)
            : base(searchTicketApiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            var message = new ViewModelMessage { IsSearhEnabled = true, IsShowedSearhPanel = true, IsShowingProgress = true };
            var flyoutMsg = new FlyoutMessage 
            { 
                TypeViewModel = typeof(SimpleSearchViewModel), 
                Header = String.Concat(Resources.Search, " ", Resources.Airlines),
                IsRightPosition = true };

            DispatcherHelper.CheckBeginInvokeOnUI(() => 
            {
                MessengerInstance.Send<FlyoutMessage>(flyoutMsg);
                MessengerInstance.Send<ViewModelMessage>(message);
            });

            _cities = await _cacheService.GetAsync<IEnumerable<City>>(DataNames.Cities);
            _airlines = await _cacheService.GetAsync<IEnumerable<Airline>>(DataNames.Airlines);

            await base.InitializeAsync();

            message.IsShowingProgress = false;
            DispatcherHelper.CheckBeginInvokeOnUI(() => MessengerInstance.Send<ViewModelMessage>(message));
        }

        protected override async Task<IEnumerable<Tuple<string, int>>> UpdateCollection(ISearchQuery searchQuery)
        {
            var query = searchQuery as TextSearchQuery;
            if (query == null && !query.IsValidate)
            {
                SendError(Resources.ErrorParams);
                return null;
            }

            Airline airline = null;
            string text = query.Text;
            if (text.Length == 2)
            {
                var airlines = _airlines.Where(a => a.Code != null && a.Code.Equals(text, StringComparison.OrdinalIgnoreCase));
                if (airlines.Count() > 1)
                {
                    var airlineList = String.Join("; ", airlines.Select(a => a.Name));
                    SendError(String.Format(Resources.FormatControlledDuplicates, airlineList));

                    return null;
                }

                airline = airlines.FirstOrDefault();
            }

            if (airline == null)
            {
                var airlines = _airlines.Where(a => a.Name.ToLower().Contains(text.ToLower()));
                if (airlines.Count() > 1)
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        MessengerInstance.Send<SearchResultMessage>(
                            new SearchResultMessage
                            {
                                Message = Resources.ChooseAirline,
                                ListResult = airlines.Select(a => new AirlineViewModel(a)).OfType<IHyperlinkViewModel>()
                            });
                    });

                    return null;
                }

                airline = airlines.SingleOrDefault();
            }

            if (airline == null)
            {
                SendError(Resources.AirlineNotFound);
                return null;
            }

            TitleChart = airline.Name;

            string token = await _token.Value;

            try
            {
                var list = await _searchTicketApiFactory.PopularRoutes.GetPopularAirlineRoutes(token, airline.Code, 10);
                var fullList = list
                    .Select(t =>
                    {
                        var array = t.Item1.Split('-');
                        return new Tuple<string, string, int>(array[0], array[1], t.Item2);
                    })
                    .Join(_cities, t => t.Item1, c => c.Code, (t, c) => new Tuple<string, string, int>(c.CultureName, t.Item2, t.Item3))
                    .Join(_cities, t => t.Item2, c => c.Code, (t, c) => new Tuple<string, int>(String.Concat(t.Item1, "-", c.CultureName), t.Item3));

                return fullList;
            }
            catch (HttpRequestException ex)
            {
                SendError(Resources.Error404);
            }

            return null;
        }
    }
}