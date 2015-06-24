using Newtonsoft.Json;
using System.Collections.Generic;

namespace TravelpayoutsAPI.Library.Models.Data
{
    public sealed class City : BaseCultureDataInfo
	{
		public Coordinates Coordinates { get; set; }

		[JsonProperty("time_zone")]
		public string TimeZone { get; set; }

		[JsonProperty("country_code")]
		public string CountryCode { get; set; }
    }
}
