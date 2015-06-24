using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Data
{
    public sealed class Airline : IDataInfo
	{
		public string Name { get; set; }
		public string Alias { get; set; }

        [JsonProperty("iata")]
        public string Code { get; set; }
		public string ICAO { get; set; }
		public string Callsign { get; set; }
		public string Country { get; set; }

		[JsonProperty("is_active")]
		public bool IsActive { get; set; }
    }
}
