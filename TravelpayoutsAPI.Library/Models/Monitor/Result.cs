using System.Collections.Generic;
using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Monitor
{
    public sealed class Result<T> where T : class
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
