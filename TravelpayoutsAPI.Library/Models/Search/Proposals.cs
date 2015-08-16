using System.Collections.Generic;
using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public sealed class Proposals
    {
        /// <summary>
        /// Уникальный id билета, для объединения информации от разных агентств в один билет
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        /// Информация о стоимости перелета (цифра - id гейта)
        /// </summary>
        public Dictionary<int, TermInfo> Terms { get; set; }

        /// <summary>
        /// IATA код основной авиакомпании
        /// </summary>
        [JsonProperty("validating_carrier")]
        public string ValidatingCarrier { get; set; }

        public string[] Carriers { get; set; }

        [JsonProperty("segment_durations")]
        public int[] SegmentDurations { get; set; }

        [JsonProperty("segments_airports")]
        public List<string[]> SegmentsAirports { get; set; }

        [JsonProperty("segments_time")]
        public List<int[]> TimeSegments { get; set; }

        public List<Flights> Segment { get; set; }

        [JsonProperty("is_direct")]
        public bool IsDirect { get; set; }

        [JsonProperty("total_duration")]
        public int TotalDuration { get; set; }

        [JsonProperty("max_stops")]
        public int MaxStops { get; set; }

        [JsonProperty("max_stop_duration")]
        public int MaxStopDuration { get; set; }

        [JsonProperty("stops_airports")]
        public string[] StopsAirports { get; set; }
    }
}
