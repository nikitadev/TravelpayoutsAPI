using System;
using System.Text;

namespace TravelpayoutsAPI.Library.Models.Monitor
{
    public sealed partial class MonitorQuerySettings
    {
        public MonitorQuerySettings()
        {
        }

        public MonitorQuerySettings(string origin)
        {
            Origin = origin;
        }

        public MonitorQuerySettings(string origin, string destination = "-")
            : this(origin)
		{
            Destination = destination;
        }

        public MonitorQuerySettings(string origin, string destination = "-", DateTime? departDate = null, DateTime? returnDate = null, CurrencyType currency = CurrencyType.RUB)
            : this(origin, destination)
		{
            Currency = currency;
            DepartDate = departDate;
            ReturnDate = returnDate;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (Currency != null)
                builder.AppendFormat("currency={0}", Currency);

            if (!String.IsNullOrEmpty(Origin))
            {
                if (builder.Length != 0)
                {
                    builder.Append("&");
                }

                builder.AppendFormat("origin={0}", Origin);
            }

            if (!String.IsNullOrEmpty(Destination))
                builder.AppendFormat("&destination={0}", Destination);

            if (!String.IsNullOrEmpty(DepartMonth))
                builder.AppendFormat("&depart_date={0}", DepartMonth);

            if (!String.IsNullOrEmpty(ReturnMonth))
                builder.AppendFormat("&return_date={0}", ReturnMonth);

            if (IsShowToAffiliates != null)
                builder.AppendFormat("&show_to_affiliates={0}", IsShowToAffiliates);

            if (DepartDate != null)
                builder.AppendFormat("&depart_date={0:yyyy-MM-dd}", DepartDate);

            if (ReturnDate != null)
                builder.AppendFormat("&return_date={0:yyyy-MM-dd}", ReturnDate);

            if (!String.IsNullOrEmpty(AirlineCode))
            {
                builder.AppendFormat("airline_code={0}", AirlineCode);
            }

            if (BeginningOfPeriod != null)
            {
                builder.AppendFormat("&beginning_of_period={0:yyyy-MM-dd}", BeginningOfPeriod);
            }

            if (Period != null)
            {
                builder.AppendFormat("&period_type={0}", Period.ToString().ToLower());
            }

            if (IsOneWay != null)
            {
                builder.AppendFormat("&one_way={0}", IsOneWay);
            }

            if (Page != null)
            {
                builder.AppendFormat("&page={0}", Page);
            }

            if (Limit != null)
            {
                if (builder.Length != 0)
                {
                    builder.Append("&");
                }

                builder.AppendFormat("limit={0}", Limit);
            }

            if (Sorting != null)
            {
                string sortValue = Sorting.ToString().ToLower();
                if (Sorting == SortingMode.DistanceUnitPrice)
                {
                    sortValue = "distance_unit_price";
                }

                builder.AppendFormat("&sorting={0}", sortValue);
            }

            if (TripClass.HasValue)
            {
                builder.AppendFormat("&trip_class={0}", TripClass.Value);
            }

            if (TripDuration != null)
            {
                builder.AppendFormat("&trip_duration={0}", TripDuration);
            }

            return builder.ToString();
        }
    }
}
