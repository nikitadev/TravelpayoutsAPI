using System.Collections.Generic;

namespace TravelpayoutsAPI.Library.Models.Data
{
    public sealed class AirlineAlliance : INameDataInfo
	{
		public string Name { get; set; }
		public List<string> Airlines { get; set; }
	}
}
