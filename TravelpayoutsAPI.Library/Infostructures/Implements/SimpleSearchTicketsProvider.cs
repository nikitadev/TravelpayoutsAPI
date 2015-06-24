//*********************************************************
//
// Copyright (c) 2015 nikitadev. All rights reserved.
//
// THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
// IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
// PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//*********************************************************
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
	public enum CalendarType { departure_date, return_date }

	public class SimpleSearchTicketsProvider : BaseApiProvider, ISimpleSearchTicketsProvider
	{
        public SimpleSearchTicketsProvider(IRequestManager requestManager)
            : base(requestManager)
        {
        }

        protected override UriBuilder GetBaseUri()
        {
            var uriBuilder = new UriBuilder(GeneralSettings.ShemaName, GeneralSettings.API_URI, -1, PriceApiSettingsV1.VERSION);
            uriBuilder.Path += GeneralSettings.PRICE;

            return uriBuilder;
        }

        private IEnumerable<Ticket> GetListFromJsonData(string origin, JToken jtoken)
        {
            var result = jtoken.ToObject<Result<Ticket>>();
            if (result.IsSuccessful)
            {
                if (jtoken["data"] != null)
                {
                    var objs = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<int, Ticket>>>(jtoken["data"].ToString());
                    foreach (var element in objs)
                    {
                        foreach (var item in element.Value)
                        {
                            item.Value.Origin = origin;
                            item.Value.Destination = element.Key;

                            yield return item.Value;
                        }
                    }
                }
            }
        }

        private Dictionary<DateTime, Ticket> GetDictionaryFromJsonData(JToken jtoken)
        {
            var result = jtoken.ToObject<Result<Ticket>>();
            
            return result.IsSuccessful && jtoken["data"] != null 
                ? JsonConvert.DeserializeObject<Dictionary<DateTime, Ticket>>(jtoken["data"].ToString())
                : default(Dictionary<DateTime, Ticket>);
        }

        public async Task<IEnumerable<Ticket>> GetCheapestTickets(string token, string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null)
		{
			var fullURI = CreateUri(PriceApiSettingsV1.CHEAP, new QuerySettings(origin, destination, departDate, returnDate));

			var jtoken = await _requestManager.GetJToken(fullURI, token);

            return GetListFromJsonData(origin, jtoken);
		}

        public async Task<IEnumerable<Ticket>> GetDirectFlights(string token, string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null)
		{
			var fullURI = CreateUri(PriceApiSettingsV1.DIRECT, new QuerySettings(origin, destination, departDate, returnDate));

            var jtoken = await _requestManager.GetJToken(fullURI, token);

            return GetListFromJsonData(origin, jtoken);
		}

        public async Task<Dictionary<DateTime, Ticket>> GetTicketsFromCityForAnyday(string token, string origin, string destination = null, DateTime? departDate = null, int tripDuration = 0, CalendarType calendarType = CalendarType.departure_date, DateTime? returnDate = null)
		{
			var fullURI = CreateUri(PriceApiSettingsV1.ANYDAY, new QuerySettings(origin, destination, departDate, returnDate));

            var jtoken = await _requestManager.GetJToken(fullURI, token);

            return GetDictionaryFromJsonData(jtoken);
		}

        public async Task<IEnumerable<Ticket>> GetTicketsFromCityForAnyday(string token, string origin, string[] months, int[] tripDurations, string destination = null)
        {
            var querySettings = !String.IsNullOrEmpty(destination) ? new QuerySettings(origin, destination) : new QuerySettings(origin);

            var tasks = new List<Task<JToken>>();
            foreach(int td in tripDurations)
            {
                querySettings.TripDuration = td;
                foreach (string m in months)
                {
                    querySettings.DepartMonth = m;
                    var fullUri = CreateUri(PriceApiSettingsV1.ANYDAY, querySettings);

                    tasks.Add(_requestManager.GetJToken(fullUri, token));
                }
            }

            if (tasks.Any())
            {
                var jtokens = await Task.WhenAll(tasks).ConfigureAwait(false);

                var dics = jtokens.Select(jt => GetDictionaryFromJsonData(jt));
                if (dics.Any())
                {
                    var seq = Enumerable.Empty<KeyValuePair<DateTime, Ticket>>();

                    Dictionary<DateTime, Ticket> current = null; 
                    foreach(var d in dics.SkipWhile(d => d == current))
                    {
                        seq = seq.Concat(d);
                        current = d;
                    }

                    return seq.Select(y => y.Value);
                }
            }

            return Enumerable.Empty<Ticket>();
        }

        public async Task<Dictionary<DateTime, Ticket>> GetTicketsByMontly(string token, string origin, string destination)
		{
			var fullURI = CreateUri(PriceApiSettingsV1.MONTLY, new QuerySettings(origin, destination));

            var jtoken = await _requestManager.GetJToken(fullURI, token, true);

            return GetDictionaryFromJsonData(jtoken);
		}
    }
}
