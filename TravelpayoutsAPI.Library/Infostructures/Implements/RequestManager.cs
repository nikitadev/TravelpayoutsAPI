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
using System.Diagnostics.Contracts;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
    public class RequestManager : IRequestManager
    {
        public async Task<string> Get(Uri uri, string token, bool isGzip)
        {
            string url = uri.ToString();
            string result;
            using (var httpClient = new HttpClient())
            {
                if (!String.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Access-Token", token);
                }

                if (isGzip)
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");

                    using (var responseStream = await httpClient.GetStreamAsync(url))
                    using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                    using (var streamReader = new StreamReader(decompressedStream))
                    {
                        result = await streamReader.ReadToEndAsync();
                    }
                }
                else
                {
                    result = await httpClient.GetStringAsync(url);
                }
            }

            return result;
        }

        public async Task<T> GetObject<T>(Uri uri, string token, bool isGzip)
        {
            string json = await Get(uri, token, isGzip);

            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<JToken> GetJToken(Uri uri, string token, bool isGzip)
        {
            string json = await Get(uri, token, isGzip);

            return JContainer.Parse(json);
        }
    }
}
