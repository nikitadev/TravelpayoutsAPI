using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelpayoutsAPI.Library
{
    internal static class PriceApiSettingsV2
	{
		public const string VERSION = "/v2";

		// Цены на авиабилеты
		public const string LATEST = "latest";
		// Календарь цен на месяц
		public const string MONTHMATRIX = "month-matrix";
		// Цены по альтернативным направлениям
		public const string NEARESTMATRIX = "nearest-places-matrix";
		// Специальные предложения
		public const string SPECIALOFFER = "special-offers";
		// Календарь цен на неделю
		public const string WEEKMATRIX = "week-matrix";
		// Дешевые авиабилеты на праздничные дни
		public const string HOLYDAYS = "holidays-by-routes";
	}
}
