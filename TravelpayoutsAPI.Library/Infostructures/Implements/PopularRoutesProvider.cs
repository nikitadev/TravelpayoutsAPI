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
using System.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
    public class PopularRoutesProvider : BaseApiProvider, IPopularRoutesProvider
	{
        public PopularRoutesProvider(IRequestManager requestManager)
            : base(requestManager)
        {
        }

        protected override UriBuilder GetBaseUri()
        {
            return new UriBuilder(GeneralSettings.ShemaName, GeneralSettings.API_URI, -1, PriceApiSettingsV1.VERSION);
        }

        public async Task<IEnumerable<Tuple<string, int>>> GetPopularAirlineRoutes(string token, string airlineCode, int limit)
		{
			var fullURI = CreateUri(GeneralSettings.POPULARROUTES_1, new QuerySettings { AirlineCode = airlineCode, Limit = limit });

			var jtoken = await _requestManager.GetJToken(fullURI, token, false);
            var result = jtoken.ToObject<Result<Tuple<string, int>>>();

			if (result.IsSuccessful)
			{
				var objs = jtoken["data"];
                var dic = objs.ToObject<Dictionary<string, int>>();

                result.Data = dic.Select(i => new Tuple<string, int>(i.Key, i.Value)).ToList();
			}

			return result.Data;
		}

        public async Task<IEnumerable<Ticket>> GetPopularRoutesFromCity(string token, string origin)
		{
			var fullURI = CreateUri(GeneralSettings.POPULARROUTES_2, new QuerySettings { Origin = origin });

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
    }
}
