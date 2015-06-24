using Newtonsoft.Json;
using System.Collections.Generic;

namespace TravelpayoutsAPI.Library.Models.Data
{
	public sealed class PlaneRoute
	{
		[JsonProperty("airline_iata")]
		public string AirlineIATA { get; set; }

		[JsonProperty("airline_icao")]
		public string AirlineICAO { get; set; }

		[JsonProperty("departure_airport_iata")]
		public string DepartureAirportIATA { get; set; }

		[JsonProperty("departure_airport_icao")]
		public string DepartureAirportICAO { get; set; }

		[JsonProperty("arrival_airport_iata")]
		public string ArrivalAirportIATA { get; set; }

		[JsonProperty("arrival_airport_icao")]
		public string ArrivalAirportICAO { get; set; }

		public bool Codeshare { get; set; }
		public int Transfers { get; set; }
		public List<string> Planes { get; set; }

	}
}
