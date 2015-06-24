using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Data
{
	public sealed class Coordinates
	{
		[JsonProperty("lon")]
		public double Longitude { get; set; }

		[JsonProperty("lat")]
		public double Latitude { get; set; }
	}
}
