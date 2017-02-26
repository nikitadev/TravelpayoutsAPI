using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Models.Data;
using TravelpayoutsAPI.Library.Models.Monitor;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CheapTicketsViewModel : BasePageSearchResultViewModel<Ticket>
    {
        private IEnumerable<City> _cities;

        /// <summary>
        /// Initializes a new instance of the CheapTiketsViewModel class.
        /// </summary>
        public CheapTicketsViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            var message = new ViewModelMessage { IsSearhEnabled = true, IsShowedSearhPanel = true, IsShowingProgress = true };
            var flyoutMsg = new FlyoutMessage
            {
                TypeViewModel = typeof(SearchViewModel),
                Header = Resources.Search,
                IsRightPosition = true
            };

            MessengerInstance.Send(flyoutMsg);
            MessengerInstance.Send(message);

            _cities = await _cacheService.GetAsync<IEnumerable<City>>(DataNames.Cities);

            await base.InitializeAsync();

            message.IsShowingProgress = false;
            MessengerInstance.Send(message);
        }

        protected override async Task<IEnumerable<Ticket>> UpdateCollection(ISearchQuery searchQuery)
        {
            var query = searchQuery as SearchQuery;
            if (query == null)
            {
                SendError(Resources.ErrorParams);
                return null;
            }

            var info = await _apiInfo.Value;
            string token = info.Item1;

            try
            {
                IEnumerable<Ticket> tickets;
                if (query.Destination != null && query.DepartDate.HasValue && query.ReturnDate.HasValue)
                {
                    tickets = await _apiFactory.SimpleSearch
                        .GetCheapestTickets(token, query.Original.Code, query.Destination.Code, query.DepartDate, query.ReturnDate);
                }
                else
                {
                    tickets = await _apiFactory.SimpleSearch
                        .GetCheapestTickets(token, query.Original.Code, query.Destination != null ? query.Destination.Code : null);
                }

                var list = tickets.ToList();
                foreach (var ticket in list)
                {
                    var origin = _cities.SingleOrDefault(c => c.Code.Equals(ticket.Origin));
                    var destination = _cities.SingleOrDefault(c => c.Code.Equals(ticket.Destination));
                    
                    ticket.Origin = origin.CultureName;
                    ticket.Destination = destination.CultureName;
                }

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