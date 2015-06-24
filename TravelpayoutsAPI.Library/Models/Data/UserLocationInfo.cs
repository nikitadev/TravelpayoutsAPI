using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Data
{
    public sealed class UserLocationInfo : IDataInfo
	{
		[JsonProperty("iata")]
		public string Code { get; set; }
		public string Name { get; set; }
		public string IP { get; set; }
	}
}
