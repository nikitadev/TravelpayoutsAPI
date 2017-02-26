using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models;
using TravelpayoutsAPI.Library.Models.Monitor;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CalendarTicketsViewModel : BasePageChartViewModel<Tuple<string, double>>
    {
        /// <summary>
        /// Initializes a new instance of the CalendarTiketsViewModel class.
        /// </summary>
        public CalendarTicketsViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            var message = new ViewModelMessage { IsSearhEnabled = true, IsShowedSearhPanel = true, IsShowingProgress = true };
            var flyoutMsg = new FlyoutMessage 
            { 
                TypeViewModel = typeof(SearchByMonthViewModel),
                Header = String.Concat(Resources.Search, " ", Resources.Tickets), 
                IsRightPosition = true 
            };

            MessengerInstance.Send(flyoutMsg);
            MessengerInstance.Send(message);

            await base.InitializeAsync();

            message.IsShowingProgress = false;
            MessengerInstance.Send(message);
        }

        protected override async Task<IEnumerable<Tuple<string, double>>> UpdateCollection(ISearchQuery searchQuery)
        {
            var query = searchQuery as SearchByMonthQuery;
            if (query == null)
            {
                SendError(Resources.ErrorParams);
                return null;
            }

            var info = await _apiInfo.Value;
            string token = info.Item1;

            try
            {
                var months = query.GetMonths();
                var durations = query.GetDurations();

                var collection = query.Destination != null 
                    ? await _apiFactory.SimpleSearch.GetTicketsFromCityForAnyday(token, query.Original.Code, months, durations, query.Destination.Code)
                    : await _apiFactory.SimpleSearch.GetTicketsFromCityForAnyday(token, query.Original.Code, months, durations);

                var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                var list = collection.AsParallel()
                    .OrderBy(k => k.Departure.Month)
                    .GroupBy(k => dateTimeFormat.GetMonthName(k.Departure.Month))
                    .Select(g => new Tuple<string, double>(g.Key, g.Max(t => t.Price)));
                    
                return list;
            }
            catch (HttpRequestException)
            {
                SendError(Resources.Error404);
            }

            return null;
        }
    }
}