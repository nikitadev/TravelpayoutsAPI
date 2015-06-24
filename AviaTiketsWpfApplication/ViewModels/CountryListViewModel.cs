using System.Linq;
using AviaTicketsWpfApplication.Fundamentals;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Models.Data;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.ViewModels.Data;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

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
        public CountryListViewModel(ISearchTicketApiFactory searchTicketApiFactory, ICacheService cacheService)
            : base(searchTicketApiFactory, cacheService)
        {
        }

        protected override async Task<IEnumerable<CountryViewModel>> UpdateCollection()
        {
            var list = await _cacheService.GetAsync<IEnumerable<Country>>(DataNames.Countries);

            return list.OrderBy(c => c.CultureName.First()).Select(c => new CountryViewModel(c));
        }
    }
}