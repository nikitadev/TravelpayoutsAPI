using System.Collections.Generic;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Models.Monitor;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class SpecialOffersViewModel : BaseCollectionViewModel<Offer>
    {
        public SpecialOffersViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override async Task<IEnumerable<Offer>> UpdateCollection()
        {
            return await _apiFactory.MainSearch.GetSpecialOffers();
        }
    }
}