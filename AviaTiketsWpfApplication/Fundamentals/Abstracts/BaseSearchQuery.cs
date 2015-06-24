using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.Fundamentals.Abstracts
{
    public abstract class BaseSearchQuery : ISearchQuery
    {
        public City Original;
        public City Destination;

        public virtual bool IsValidate
        {
            get { return Original != null; }
        }
    }
}
