using System;
using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public sealed class Flights
    {
        public Flight[] Flight { get; set; }
    }

    public sealed class Flight
    {
        public string Departure { get; set; }

        public string Arrival { get; set; }

        public int Number { get; set; }

        public int Duration { get; set; }

        [JsonProperty("arrival_date")]
        public DateTime ArrivalDate { get; set; }

        [JsonProperty("arrival_time")]
        public TimeSpan ArrivalTime { get; set; }

        [JsonProperty("departure_time")]
        public TimeSpan DepartureTime { get; set; }

        [JsonProperty("departure_date")]
        public DateTime DepartureDate { get; set; }

        [JsonProperty("departure_timestamp")]
        public string DepartureTimestamp { get; set; }

        [JsonProperty("local_departure_timestamp")]
        public string LocalDepartureTimestamp { get; set; }

        [JsonProperty("arrival_timestamp")]
        public string ArrivalTimestamp { get; set; }

        [JsonProperty("local_arrival_timestamp")]
        public string LocalArrivalTimestamp { get; set; }

        public string Aircraft { get; set; }

        public int Delay { get; set; }

        [JsonProperty("operating_carrier")]
        public string OperatingCarrier { get; set; }
    }
}
