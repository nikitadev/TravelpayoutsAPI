using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Data
{
    public sealed class Airport : BaseCultureDataInfo
	{
		public Coordinates Coordinates { get; set; }

		[JsonProperty("time_zone")]
		public string TimeZone { get; set; }

		[JsonProperty("country_code")]
		public string CountryCode { get; set; }

		[JsonProperty("city_code")]
		public string CityCode { get; set; }
    }
}
