using System;

namespace TravelpayoutsAPI.Library
{
    internal static class GeneralSettings
    {
        public const string ShemaName = "http";

        public const string MAIN_URI = "www.travelpayouts.com";

		public const string IP_URI = "api.ipify.org";
        public const string WAI = "/whereami";

		public const string API_URI = "api.travelpayouts.com";

		public const string PRICE = "/prices/";

        // Поиск в реальном времени
        public const string FLIGHT_SEARCH = "/flight_search";

        // Результат поиска в реальном времени
        public const string FLIGHT_SEARCH_RESULT = "/flight_search_results";

        // Популярные направления авиакомпании
        public const string POPULARROUTES_1 = "/airline-directions";

		// Популярные направления из города
		public const string POPULARROUTES_2 = "/city-directions";

		// Данные: Planes, Countries, Airports, Airlines, Alliances, Cities, Routes, Airline, Airline
		public const string DATA = "/data/";
	}
}
