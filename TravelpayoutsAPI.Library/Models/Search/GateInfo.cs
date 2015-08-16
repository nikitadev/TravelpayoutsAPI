using Newtonsoft.Json;

namespace TravelpayoutsAPI.Library.Models.Search
{
    /// <summary>
    /// информация об агенте
    /// </summary>
    public sealed class GateInfo
    {
        /// <summary>
        /// код валюты оплаты
        /// </summary>
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }

        /// <summary>
        /// является ли авиакомпанией
        /// </summary>
        [JsonProperty("is_airline")]
        public bool IsAirline { get; set; }

        /// <summary>
        /// средний рейтинг агентства
        /// </summary>
        [JsonProperty("average_rate")]
        public double AverageRate { get; set; }

        /// <summary>
        /// рейтинг агентства (кол-во проголосовавших)
        /// </summary>
        public int Rates { get; set; }

        /// <summary>
        /// наличие мобильной версии сайта
        /// </summary>
        [JsonProperty("mobile_version")]
        public bool HasMobileVersion { get; set; }

        /// <summary>
        /// производительность
        /// </summary>
        public string Productivity { get; set; }

        /// <summary>
        /// IATA код авиакомпании, если билеты продает она сама
        /// </summary>
        [JsonProperty("airline_iatas")]
        public string[] AirlineIATAs { get; set; }

        /// <summary>
        /// способы оплаты
        /// </summary>
        [JsonProperty("payment_methods")]
        public string[] PaymentMethods { get; set; }

        /// <summary>
        /// название агентства
        /// </summary>
        public string Label { get; set; }

        [JsonProperty("working_hours")]
        public string WorkingHours { get; set; }

        public string Helplink { get; set; }

        public string Email { get; set; }

        public string[] Phone { get; set; }
    }
}
