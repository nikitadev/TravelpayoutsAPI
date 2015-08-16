using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Monitor
{
    public class LabelTiket : Ticket
    {
        [JsonProperty("show_to_affiliates")]
        public bool ShowToAffiliates { get; set; }

        [JsonProperty("trip_class")]
        public int TripClass { get; set; }

        [JsonProperty("number_of_changes")]
        public int NumberOfChanges { get; set; }

        public int Value { get; set; }

        [JsonProperty("found_at")]
        public string found_at { get; set; }
        public int Distance { get; set; }

        [JsonProperty("actual")]
        public bool IsActual { get; set; }
    }
}
