namespace TravelpayoutsAPI.Library
{
	internal static class PriceApiSettingsV1
	{
		public const string VERSION = "/v1";

		// Билеты из города на любое число месяца
		public const string ANYDAY = "calendar";
		// Самые дешевые билеты
		public const string CHEAP = "cheap";
		// Билеты без пересадок
		public const string DIRECT = "direct";
		// Цены на билеты по месяцам
		public const string MONTLY = "monthly";
    }
}
