using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.ViewModels.Data;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class AirportListViewModel : BaseCollectionViewModel<AirportViewModel>
    {
        public AirportListViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override async Task<IEnumerable<AirportViewModel>> UpdateCollection()
        {
            var list = await _cacheService.GetAsync<IEnumerable<Airport>>(DataNames.Airports);

            return list.OrderBy(a => a.Name.First()).Select(a => new AirportViewModel(a));
        }
    }
}