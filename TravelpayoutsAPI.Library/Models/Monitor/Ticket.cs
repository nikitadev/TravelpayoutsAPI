using System;
using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Monitor
{
    /// <summary>
    /// Данные по авиабилету
    /// </summary>
    /// <remarks>
    /// "origin":"MOW",
    /// "destination":"LED"
    /// "price": 27506,
    /// "transfers":0,
    /// "airline": "CX",
    /// "flight_number": 204,
    /// "departure_at": "2015-06-05T16:40:00Z",
    /// "return_at": "2015-06-22T12:00:00Z",
    /// "expires_at": "2015-01-08T18:38:45Z"
    /// </remarks>
    public class Ticket
	{
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Transfers { get; set; }
		public double Price { get; set; }
		public string Airline { get; set; }

		[JsonProperty("flight_number")]
		public int FlightNumber { get; set; }

		[JsonProperty("departure_at")]
		public DateTime Departure { get; set; }

		[JsonProperty("return_at")]
		public DateTime Return { get; set; }

		[JsonProperty("expires_at")]
		public DateTime Expires { get; set; }
	}
}
