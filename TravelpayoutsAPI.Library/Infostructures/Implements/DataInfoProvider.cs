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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library.Models.Data;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
    public sealed class DataInfoProvider : BaseApiProvider, IDataInfoProvider
    {
        public DataInfoProvider(IRequestManager requestManager)
            : base(requestManager)
        {
        }

        protected override UriBuilder GetBaseUri()
        {
            return new UriBuilder(GeneralSettings.ShemaName, GeneralSettings.API_URI, -1);
        }

		public async Task<string> GetJsonAsync(string dataName)
		{
            var uri = CreateUri(String.Concat(GeneralSettings.DATA, dataName, ".json"));

            return await _requestManager.Get(uri).ConfigureAwait(false);
		}

		public async Task<IEnumerable<T>> GetAsync<T>(string dataName) where T : class, INameDataInfo
		{
            string json = await GetJsonAsync(dataName);
			var collection = JsonConvert.DeserializeObject<IEnumerable<T>>(json);

			return collection;
        }
    }
}
