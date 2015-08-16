using System;
using System.Text.RegularExpressions;
using David.Utility.Cryptography;
using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public static class FlightSearchQuerySettingsEx
    {
        public static void SetSignature(this FlightSearchQuerySettings postQuery, string token)
        {
            //var settings = new JsonSerializerSettings
            //{
            //    NullValueHandling = NullValueHandling.Ignore,
            //    DateFormatString = "yyyy-MM-dd"
            //};

            string json = JsonConvert.SerializeObject(postQuery);

            var regex = new Regex(@"([\""\w\""]+:)|([{*?}])|([\[*?\]])|(\"")");
            json = regex.Replace(json, String.Empty).Replace(",", ":");

            string result = String.Concat(token, ":", json);

            var cryptography = new CryptographyHelper();
            postQuery.Signature = cryptography.GetMd5Hash(result);
        }
    }
}
