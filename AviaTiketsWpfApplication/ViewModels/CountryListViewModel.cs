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
    public sealed class CountryListViewModel : BaseCollectionViewModel<CountryViewModel>
    {
        public CountryListViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override async Task<IEnumerable<CountryViewModel>> UpdateCollection()
        {
            var list = await _cacheService.GetAsync<IEnumerable<Country>>(DataNames.Countries);

            return list.OrderBy(c => c.CultureName.First()).Select(c => new CountryViewModel(c));
        }
    }
}