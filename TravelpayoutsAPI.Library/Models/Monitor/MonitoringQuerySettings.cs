using System;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace TravelpayoutsAPI.Library.Models.Monitor
{
    /// <summary>
    /// Валюта цен
    /// </summary>
    public enum CurrencyType { USD, EUR, RUB }

	/// <summary>
	/// Тип периода: Year - за всё время, Month - за месяц, Seasson - за сезон (3 месяца), Day - по дням
	/// </summary>
	public enum PeriodType { Day, Year, Month, Season }

	/// <summary>
	/// Price - по цене, 
	/// Route - по популярности маршрута, 
	/// DistanceUnitPrice - по цене за километр
	/// </summary>
	public enum SortingMode { Price, Route, DistanceUnitPrice }

	/// <summary>
	/// Класс перелёта 0 - Эконом, 1 - Бизнес, 2 - Первый.
	/// </summary>
	public enum TripClassMode { Econom, Buissnes, First }

	public sealed partial class MonitorQuerySettings : IQuerySettings
    {
		public CurrencyType? Currency { get; set; }
		public string Origin { get; set; }
		public string Destination { get; set; }
		public DateTime? DepartDate { get; set; }
		public DateTime? ReturnDate { get; set; }
        public string DepartMonth { get; set; }
        public string ReturnMonth { get; set; }
		public bool? IsShowToAffiliates { get; set; }
		public string AirlineCode { get; set; }
		public int? Limit { get; set; }
		public DateTime Month { get; set; }
        public DateTime? BeginningOfPeriod { get; set; }
		public PeriodType? Period { get; set; }
		public bool? IsOneWay { get; set; }
		public int? Page { get; set; }
		public SortingMode? Sorting { get; set; }
		public TripClassMode? TripClass { get; set; }
		public int? TripDuration { get; set; }
	}
}
