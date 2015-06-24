using System;
using System.Text;

namespace TravelpayoutsAPI.Library.Models
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

	public class QuerySettings
	{
		public QuerySettings()
		{
		}

        public QuerySettings(string origin)
        {
            Origin = origin;
        }

		public QuerySettings(string origin, string destination = "-")
            : this(origin)
		{
			Destination = destination;
		}

		public QuerySettings(string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null, CurrencyType currency = CurrencyType.RUB)
            : this(origin, destination)
		{
			Currency = currency;
			DepartDate = departDate;
			ReturnDate = returnDate;
        }

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

        

		public override string ToString()
		{
			var builder = new StringBuilder();

			if (Currency != null)
				builder.Append(String.Concat("currency=", Currency.ToString()));

			if (!String.IsNullOrEmpty(Origin))
			{
				if (builder.Length != 0)
				{
					builder.Append("&");
                }

				builder.Append(String.Concat("origin=", Origin));
			}

			if (!String.IsNullOrEmpty(Destination))
				builder.Append(String.Concat("&destination=", Destination));

            if (!String.IsNullOrEmpty(DepartMonth))
                builder.Append(String.Concat("&depart_date=", DepartMonth));

            if (!String.IsNullOrEmpty(ReturnMonth))
                builder.Append(String.Concat("&return_date=", ReturnMonth));

			if (IsShowToAffiliates != null)
				builder.Append(String.Concat("&show_to_affiliates=", IsShowToAffiliates));

			if (DepartDate != null)
				builder.Append(String.Concat("&depart_date=", DepartDate));

			if (ReturnDate != null)
				builder.Append(String.Concat("&return_date=", ReturnDate));

			if (!String.IsNullOrEmpty(AirlineCode))
			{
				builder.Append(String.Concat("airline_code=", AirlineCode));
			}

			if (BeginningOfPeriod != null)
			{
                builder.Append(String.Format("&beginning_of_period={0:yyyy-MM-dd}", BeginningOfPeriod));
			}

			if (Period != null)
			{
				builder.Append(String.Concat("&period_type=", Period.ToString().ToLower()));
			}

			if (IsOneWay != null)
			{
				builder.Append(String.Concat("&one_way=", IsOneWay));
			}

			if (Page != null)
			{
				builder.Append(String.Concat("&page=", Page));
			}

            if (Limit != null)
            {
                if (builder.Length != 0)
                {
                    builder.Append("&");
                }

                builder.Append(String.Concat("limit=", Limit));
            }

			if (Sorting != null)
			{
                string sortValue = Sorting.ToString().ToLower();
                if (Sorting == SortingMode.DistanceUnitPrice)
                {
                    sortValue = "distance_unit_price";
                }
                
				builder.Append(String.Concat("&sorting=", sortValue));
			}

			if (TripClass != null)
			{
				builder.Append(String.Concat("&trip_class=", (int)TripClass));
			}

			if (TripDuration != null)
			{
				builder.Append(String.Concat("&trip_duration=", TripDuration));
			}

			return builder.ToString();
		}
	}
}
