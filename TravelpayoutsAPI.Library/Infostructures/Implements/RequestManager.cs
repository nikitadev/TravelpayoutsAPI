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
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace TravelpayoutsAPI.Library.Infostructures.Implements
{
    public class RequestManager : IRequestManager
    {
        public async Task<string> Get(Uri uri, string token, bool isGzip)
        {
            string url = uri.ToString();
            using (var httpClient = new HttpClient())
            {
                if (!String.IsNullOrEmpty(token))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-Access-Token", token);
                }

                if (isGzip)
                {
                    string encodingKey = "Accept-Encoding";
                    try
                    {
                        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(encodingKey, "gzip, deflate");

                        using (var responseStream = await httpClient.GetStreamAsync(url))
                        using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                        using (var streamReader = new StreamReader(decompressedStream))
                        {
                            return await streamReader.ReadToEndAsync();
                        }
                    }
                    catch (InvalidDataException)
                    {
                        httpClient.DefaultRequestHeaders.Remove(encodingKey);
                    }
                }

                return await httpClient.GetStringAsync(url);
            }
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

        public async Task<string> Post<T>(Uri uri, T data) where T : class
        {
            string url = uri.ToString();
            string result;
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = uri;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(data);

                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
                //stringContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

                var message = await httpClient.PostAsync(url, stringContent);

                // message.EnsureSuccessStatusCode();
                result = await message.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
