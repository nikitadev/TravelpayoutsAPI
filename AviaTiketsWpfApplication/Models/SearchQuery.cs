using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using System;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.Models
{
    public sealed class SearchQuery : BaseSearchQuery
    {
        public City Original;
        public City Destination;
        public DateTime? DepartDate;
        public DateTime? ReturnDate;

        public bool IsValidate
        {
            get
            {
                return Original != null;
            }
        }
    }
}
