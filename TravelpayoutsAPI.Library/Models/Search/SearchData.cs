using System.Collections.Generic;
using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public sealed class SearchData
    {
        [JsonProperty("search_id")]
        public string SearchId { get; set; }

        public string Affiliate { get; set; }

        [JsonProperty("geoip_country")]
        public string GeoIPCountry { get; set; }

        [JsonProperty("geoip_city")]
        public string GeoIPCity { get; set; }

        [JsonProperty("gates_count")]
        public int GatesCount { get; set; }

        [JsonProperty("currency_rates")]
        public Dictionary<string, decimal> CurrencyRates { get; set; }
    }
}
