using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TravelpayoutsAPI.Library.Models
{
	public class Result<T> where T : class
	{
		public Result()
		{
			Data = new List<T>();
        }

		[JsonProperty("success")]
		public bool IsSuccessful { get; set; }

        [JsonIgnore]
        public List<T> Data { get; set; }
	}
}
