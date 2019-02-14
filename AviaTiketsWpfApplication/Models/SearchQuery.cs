using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using System;

namespace AviaTicketsWpfApplication.Models
{
    public sealed class SearchQuery : BaseSearchQuery
    {
        public DateTime? DepartDate;
        public DateTime? ReturnDate;
    }
}
