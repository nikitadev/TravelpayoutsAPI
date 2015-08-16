using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using System;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.Models
{
    public sealed class SearchQuery : BaseSearchQuery
    {
        public DateTime? DepartDate;
        public DateTime? ReturnDate;

        public override bool IsValidate
        {
            get
            {
                return Original != null;
            }
        }
    }
}
