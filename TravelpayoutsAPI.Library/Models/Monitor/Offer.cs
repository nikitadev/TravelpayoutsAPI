using System;
using System.Linq;
using System.Xml.Serialization;
using TravelpayoutsAPI.Library.Infostructures;

namespace TravelpayoutsAPI.Library.Models.Monitor
{
    [XmlRoot("route")]
    public class Route
    {
        [XmlAttribute("from_iata")]
        public string FromIATA { get; set; }

        [XmlAttribute("to_iata")]
        public string ToIATA { get; set; }

        [XmlAttribute("from_name")]
        public string FromName { get; set; }

        [XmlAttribute("to_name")]
        public string ToName { get; set; }

        [XmlAttribute("class")]
        public string Class { get; set; }

        [XmlAttribute("oneway_price")]
        public string OnewayPrice { get; set; }

        [XmlAttribute("roundtrip_price")]
        public string RoundtripPrice { get; set; }
    }

    [XmlRoot("offer")]
    public class Offer
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("airline")]
        public string Airline { get; set; }

        [XmlAttribute("airline_code")]
        public string AirlineCode { get; set; }

        [XmlAttribute("title")]
        public string Title { get; set; }

        [XmlAttribute("sale_date_begin")]
        public long SaleDateBegin { get; set; }

        [XmlAttribute("sale_date_end")]
        public long SaleDateEnd { get; set; }

        [XmlAttribute("flight_date_begin")]
        public long FlightDateBegin { get; set; }

        [XmlAttribute("flight_date_end")]
        public long FlightDateEnd { get; set; }

        [XmlAttribute("link")]
        public string Link { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("route")]
        public Route[] Routes { get; set; }

        [XmlIgnore]
        public DateTime FlightBegin { get { return FlightDateBegin.ToDateTime(); } }

        [XmlIgnore]
        public DateTime FlightEnd { get { return FlightDateEnd.ToDateTime(); } }

        [XmlIgnore]
        public DateTime SaleBegin { get { return SaleDateBegin.ToDateTime(); } }

        [XmlIgnore]
        public DateTime SaleEnd { get { return SaleDateEnd.ToDateTime(); } }

        [XmlIgnore]
        public string MinPrice { get { return Routes.Any() ? Routes.First().RoundtripPrice : String.Empty; } }
    }
}
