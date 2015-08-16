using System.Collections.Generic;
using Newtonsoft.Json;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public class FlightSearchQuerySettings : IQuerySettings
    {
        public string Signature { get; internal set; }

        public string Host { get; set; }
        public string Locale { get; set; }
        public string Marker { get; set; }

        public Passengers Passengers { get; set; }

        public List<Segment> Segments { get; set; }

        [JsonProperty("trip_class")]
        public string TripClass { get; set; }

        [JsonProperty("user_ip")]
        public string UserIP { get; set; }
    }
}
