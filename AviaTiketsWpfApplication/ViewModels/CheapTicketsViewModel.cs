using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models;
using TravelpayoutsAPI.Library.Models.Data;

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
        public CheapTicketsViewModel(ISearchTicketApiFactory searchTicketApiFactory, ICacheService cacheService)
            : base(searchTicketApiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            var message = new ViewModelMessage { IsSearhEnabled = true, IsShowedSearhPanel = true, IsShowingProgress = true };

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                MessengerInstance.Send<FlyoutMessage>(new FlyoutMessage { TypeViewModel = typeof(SearchViewModel), Header = "Search tickets", IsRightPosition = true });
                MessengerInstance.Send<ViewModelMessage>(message);
            });

            _cities = await _cacheService.GetAsync<IEnumerable<City>>(DataNames.Cities);

            await base.InitializeAsync();

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                message.IsShowingProgress = false;
                MessengerInstance.Send<ViewModelMessage>(message);
            });
        }

        protected override async Task<IEnumerable<Ticket>> UpdateCollection(ISearchQuery searchQuery)
        {
            var query = searchQuery as SearchQuery;
            if (query == null)
            {
                SendError("Error in parametrs.");
                return null;
            }

            var token = await _token.Value;

            try
            {
                IEnumerable<Ticket> tickets;
                if (query.Destination != null && query.DepartDate.HasValue && query.ReturnDate.HasValue)
                {
                    tickets = await _searchTicketApiFactory.SimpleSearch
                        .GetCheapestTickets(token, query.Original.Code, query.Destination.Code, query.DepartDate, query.ReturnDate);
                }
                else
                {
                    tickets = await _searchTicketApiFactory.SimpleSearch
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
            catch (HttpRequestException ex)
            {
                SendError("Not found. Try to change criteria search and found again.");
            }

            return null;
        }
    }
}