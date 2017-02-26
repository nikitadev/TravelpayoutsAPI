using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Models.Data;
using TravelpayoutsAPI.Library.Models.Search;

namespace AviaTicketsWpfApplication.ViewModels
{
    public sealed class RealtimeTicketsViewModel : BasePageSearchResultViewModel<SearchResult>
    {
        public RealtimeTicketsViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override Task InitializeAsync()
        {
            var message = new ViewModelMessage { IsSearhEnabled = true, IsShowedSearhPanel = true };
            var flyoutMsg = new FlyoutMessage
            {
                TypeViewModel = typeof(RealtimeSearchViewModel),
                Header = Resources.Search,
                IsRightPosition = true
            };

            MessengerInstance.Send(flyoutMsg);
            MessengerInstance.Send(message);

            return base.InitializeAsync();
        }

        protected override async Task<IEnumerable<SearchResult>> UpdateCollection(ISearchQuery searchQuery)
        {
            var query = searchQuery as RealtimeSearchQuery;
            if (query == null)
            {
                SendError(Resources.ErrorParams);
                return null;
            }

            var userInfo = await _cacheService.GetAsync<UserLocationInfo>(CacheTags.USERINFO);

            var info = await _apiInfo.Value;
            try
            {
                var searchResults = await _apiFactory.RealtimeSearch
                        .GetTickets(info.Item1, info.Item2, Settings.Default.Host, query.Adults.Value, query.Children.Value, query.Infants.Value, query.TripClass, userInfo.IP, query.GetSegments());

                //var list = tickets.ToList();
                //foreach (var ticket in list)
                //{
                //    var origin = _cities.SingleOrDefault(c => c.Code.Equals(ticket.Origin));
                //    var destination = _cities.SingleOrDefault(c => c.Code.Equals(ticket.Destination));

                //    ticket.Origin = origin.CultureName;
                //    ticket.Destination = destination.CultureName;
                //}

                return searchResults;//searchResults.SelectMany(q => q.Proposals);
            }
            catch (HttpRequestException)
            {
                SendError(Resources.Error404);
            }

            return null;
        }
    }
}
