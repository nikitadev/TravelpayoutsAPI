using System;
using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public sealed class Segment
    {
        public DateTime Date { get; set; }

        public string Destination { get; set; }

        public string Origin { get; set; }

        [JsonProperty("original_destination")]
        public string OriginalDestination { get; set; }

        [JsonProperty("original_origin")]
        public string OriginalOrigin { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }

        [JsonProperty("destination_country")]
        public string DestinationCountry { get; set; }
    }
}
