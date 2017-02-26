using System;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using TravelpayoutsAPI.Library.Models.Search;

namespace AviaTicketsWpfApplication.Models
{
    public sealed class RealtimeSearchQuery : BaseSearchQuery, ISearchQuery
    {
        public DateTime? DepartDate;
        public DateTime? ReturnDate;

        public int? Adults;

        public int? Children;

        public int? Infants;

        public string TripClass;

        public RealtimeSearchQuery()
        {
            TripClass = "Y";
        }

        public Segment[] GetSegments()
        {
            return new Segment[]
            {
                new Segment { Date = DepartDate.Value, Origin = Original.Code, Destination = Destination.Code },
                new Segment { Date = ReturnDate.Value, Origin = Destination.Code, Destination = Original.Code }
            };
        }
    }
}
