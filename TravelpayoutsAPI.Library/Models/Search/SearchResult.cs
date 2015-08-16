using System.Collections.Generic;
using Newtonsoft.Json;
using TravelpayoutsAPI.Library.Models.Data;

namespace TravelpayoutsAPI.Library.Models.Search
{
    public sealed class SearchResult
    {
        /// <summary>
        /// идентификатор поиска
        /// </summary>
        [JsonProperty("search_id")]
        public string SearchId { get; set; }

        /// <summary>
        /// номера рейсов
        /// </summary>
        [JsonProperty("flight_numbers")]
        public List<string[]> FlightNumbers { get; set; }

        /// <summary>
        /// данные по агенствам
        /// </summary>
        public MetaInfo Meta { get; set; }

        /// <summary>
        /// расстояние между городам отправления и назначения
        /// </summary>
        [JsonProperty("city_distance")]
        public int CityDistance { get; set; }

        /// <summary>
        /// информация об агенте, продавце билетов (цифра - id гейта)
        /// </summary>
        [JsonProperty("gates_info")]
        public Dictionary<int, GateInfo> GatesInfo { get; set; }

        /// <summary>
        /// сигнатура запроса
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// массив данных о перелетах
        /// </summary>
        public List<Segment> Segments { get; set; }

        /// <summary>
        /// информация об авиакомпании
        /// </summary>
        public Dictionary<string, Airline> Airlines { get; set; }

        /// <summary>
        /// Данные об аэропортах. [IATA код аэропорта, информация об аэропорте]
        /// </summary>
        public Dictionary<string, Airport> Airports { get; set; }

        /// <summary>
        /// массив найденных вариантов
        /// </summary>
        public List<Proposals> Proposals { get; set; }

        public string Currency { get; set; }

        [JsonProperty("open_jaw")]
        public bool IsOpenJaw { get; set; }

        [JsonProperty("travelpayouts_api_request")]
        public bool IsTravelpayoutsApi { get; set; }

        [JsonProperty("internal")]
        public bool IsInternal { get; set; }

        [JsonProperty("affiliate_has_sales")]
        public bool HasAffiliateSales { get; set; }
    }
}
