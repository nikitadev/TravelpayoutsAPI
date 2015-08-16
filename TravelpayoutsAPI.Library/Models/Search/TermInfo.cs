using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    /// <summary>
    /// информация о стоимости перелета
    /// </summary>
    public sealed class TermInfo
    {
        public string Price { get; set; }

        public string Currency { get; set; }

        public string Url { get; set; }

        [JsonProperty("unified_price ")]
        public decimal UnifiedPrice { get; set; }
    }
}
