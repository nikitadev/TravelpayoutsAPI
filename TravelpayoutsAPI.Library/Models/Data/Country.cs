using Newtonsoft.Json;
using System.Collections.Generic;
namespace TravelpayoutsAPI.Library.Models.Data
{
    public sealed class Country : BaseCultureDataInfo
	{
		public string Currency { get; set; }
    }
}
