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
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
	public class SearchTicketsProvider : BaseApiProvider, ISearchTicketsProvider
	{
        public SearchTicketsProvider(IRequestManager requestManager)
            : base(requestManager)
        {
        }

        protected override UriBuilder GetBaseUri()
        {
            var uriBuilder = new UriBuilder(GeneralSettings.ShemaName, GeneralSettings.API_URI, -1, PriceApiSettingsV2.VERSION);
            uriBuilder.Path += GeneralSettings.PRICE;

            return uriBuilder;
        }

		public async Task<List<Ticket>> GetPrice(string token, QuerySettings settings)
		{
			var fullURI = CreateUri(PriceApiSettingsV2.LATEST, settings);

			var jtoken = await _requestManager.GetJToken(fullURI, token, false);
			var result = jtoken.ToObject<Result<Ticket>>();

			if (result.IsSuccessful)
			{
				var objs = jtoken["data"];
				foreach (var element in objs)
				{
					var tiket = element.First.ToObject<Ticket>();
					result.Data.Add(tiket);
				}
			}

			return result.Data;
		}

		public async Task<List<Ticket>> GetPrice(
			string token, 
			string origin, 
			string destination, 
			DateTime departDate, 
            DateTime returnDate,
			PeriodType period = PeriodType.Year, 
			bool isOneWay = false, 
			int page = 1, 
			int limit = 30,
			bool isShowToAffiliates = true,
			SortingMode sorting = SortingMode.Price,
			TripClassMode tripClass = TripClassMode.Econom)
		{
			var settings = new QuerySettings(origin, destination)
			{
                BeginningOfPeriod = departDate,
				Period = period,
				IsOneWay = isOneWay,
				Page = page,
				Limit = limit,
				IsShowToAffiliates = isShowToAffiliates,
				Sorting = sorting,
				TripClass = tripClass,
			};

            var interval = returnDate - departDate;
            
            settings.TripDuration = period == PeriodType.Day ? interval.Days : interval.Days / 7;

			return await GetPrice(token, settings);
		}

		/// <summary>
		/// Календарь цен на месяц
		/// </summary>
		/// <param name="token"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="month"></param>
		/// <param name="showToAffiliates"></param>
		/// <returns></returns>
		public async Task<List<Ticket>> GetPriceOnMonth(string token, string origin, string destination, DateTime month, bool showToAffiliates = true)
		{
			var fullURI = CreateUri(PriceApiSettingsV2.MONTHMATRIX, new QuerySettings(origin, destination) { Month = month, IsShowToAffiliates = showToAffiliates });

			var jtoken = await _requestManager.GetJToken(fullURI, token, true);
			var result = jtoken.ToObject<Result<Ticket>>();

			if (result.IsSuccessful)
			{
				var objs = jtoken["data"];
				foreach (var element in objs)
				{
					var tiket = element.First.ToObject<Ticket>();
					result.Data.Add(tiket);
				}
			}

			return result.Data;
		}

		/// <summary>
		/// Цены по альтернативным направлениям
		/// </summary>
		/// <param name="token"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="showToAffiliates"></param>
		/// <param name="departDate"></param>
		/// <param name="returnDate"></param>
		/// <returns></returns>
		public async Task<List<Ticket>> GetNearestPrice(string token, string origin, string destination = "-", bool showToAffiliates = true, DateTime? departDate = null, DateTime? returnDate = null)
		{
			var fullURI = CreateUri(PriceApiSettingsV2.NEARESTMATRIX, new QuerySettings(origin, destination, departDate, returnDate) { IsShowToAffiliates = showToAffiliates });

			var jtoken = await _requestManager.GetJToken(fullURI, token, true);
			var result = jtoken.ToObject<Result<Ticket>>();

			if (result.IsSuccessful)
			{
				var objs = jtoken["data"];
				foreach (var element in objs)
				{
					var tiket = element.First.ToObject<Ticket>();
					result.Data.Add(tiket);
				}
			}

			return result.Data;
		}

		/// <summary>
		/// Календарь цен на неделю
		/// </summary>
		/// <param name="token"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="showToAffiliates"></param>
		/// <param name="departDate"></param>
		/// <param name="returnDate"></param>
		/// <returns></returns>
		public async Task<List<Ticket>> GetPriceOnWeek(string token, string origin, string destination = "-", bool showToAffiliates = true, DateTime? departDate = null, DateTime? returnDate = null)
		{
			var fullURI = CreateUri(PriceApiSettingsV2.WEEKMATRIX, new QuerySettings(origin, destination, departDate, returnDate) { IsShowToAffiliates = showToAffiliates });

			var jtoken = await _requestManager.GetJToken(fullURI, token, true);
			var result = jtoken.ToObject<Result<Ticket>>();

			if (result.IsSuccessful)
			{
				var objs = jtoken["data"];
				foreach (var element in objs)
				{
					var tiket = element.First.ToObject<Ticket>();
					result.Data.Add(tiket);
				}
			}

			return result.Data;
		}

		/// <summary>
		/// Специальные предложения
		/// </summary>
		/// <returns></returns>
        public async Task<List<Offer>> GetSpecialOffers(string token)
		{
			var fullURI = CreateUri(PriceApiSettingsV2.SPECIALOFFER);

			var xml = await _requestManager.Get(fullURI, token);

            using (var textReader = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(Offers), String.Empty);
                var result = (Offers)serializer.Deserialize(textReader);

                return result.OfferItems;
            }
		}

		/// <summary>
		/// Дешевые авиабилеты на праздничные дни
		/// </summary>
		/// <param name="token"></param>
		/// <returns></returns>
		public async Task<List<Ticket>> GetHolydayCheapest(string token)
		{
			var fullURI = CreateUri(PriceApiSettingsV2.HOLYDAYS);

			var jtoken = await _requestManager.GetJToken(fullURI, token, true);
			var result = jtoken.ToObject<Result<Ticket>>();

			if (result.IsSuccessful)
			{
				var objs = jtoken["data"];
				foreach (var element in objs)
				{
					var tiket = element.First.ToObject<Ticket>();
					result.Data.Add(tiket);
				}
			}

			return result.Data;
		}
    }
}
