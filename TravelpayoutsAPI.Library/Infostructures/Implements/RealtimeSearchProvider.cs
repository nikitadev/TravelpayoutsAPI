using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models.Search;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
    public sealed class RealtimeSearchProvider : BaseApiProvider, IRealtimeSearchProvider
    {
        private bool _canGetResult = false;

        public RealtimeSearchProvider(IRequestManager requestManager)
            : base(requestManager)
        {
        }

        protected override UriBuilder GetBaseUri()
        {
            var uriBuilder = new UriBuilder(GeneralSettings.ShemaName, GeneralSettings.API_URI, -1, PriceApiSettingsV1.VERSION);
            uriBuilder.Path += _canGetResult ? GeneralSettings.FLIGHT_SEARCH_RESULT : GeneralSettings.FLIGHT_SEARCH;

            return uriBuilder;
        }

        public async Task<List<SearchResult>> GetTickets(string token, string marker, string host, int adults, int children, int infants, string tripClass, string userIP, params Segment[] segments)
        {
            var list = new List<Segment>(segments);
            return await GetTickets(token, marker, host, adults, children, infants, tripClass, userIP, list, CancellationToken.None);
        }

        public async Task<List<SearchResult>> GetTickets(
            string token, string marker, string host, int adults, int children, int infants, string tripClass, string userIP, 
            List<Segment> segments, CancellationToken cancellationToken)
        {
            var searchResult = new List<SearchResult>();
            var fullURI = CreateUri();

            var settings = new FlightSearchQuerySettings
            {
                Host = host,
                Locale = "ru",
                Marker = marker,
                UserIP = userIP,
                TripClass = tripClass,
                Passengers = new Passengers
                {
                    Adults = adults,
                    Children = children,
                    Infants = infants
                },
                Segments = segments
            };

            settings.SetSignature(token);

            string json = await _requestManager.Post(fullURI, settings);

            var searchData = JsonConvert.DeserializeObject<SearchData>(json);
            if (searchData != null)
            {
                string searchId = searchData.SearchId;
                _canGetResult = !String.IsNullOrEmpty(searchId);

                var searchURI = CreateUri(query: String.Concat("uuid=", searchId));

                bool isSearch = true;
                while (isSearch)
                {
                    await Task.Yield();
                    cancellationToken.ThrowIfCancellationRequested();

                    //var result = await _requestManager.GetObject<List<SearchResult>>(searchURI, isGzip: true);

                    string jsonResult = await _requestManager.Get(searchURI, isGzip: true);

                    var jarray = JArray.Parse(jsonResult);

                    var last = jarray.Last;
                    if (last != null)
                    {
                        if (!(isSearch = last.Count() != 1))
                        {
                            jarray.Remove(last);
                        }
                        else if (jarray.Count == 1)
                        {
                            isSearch = false;
                        }
                    }
                    else
                    {
                        isSearch = false;
                    }

                    var collection = jarray.ToObject<List<SearchResult>>();
                    searchResult.AddRange(collection);
                }
            }

            return searchResult;
        }
    }
}
