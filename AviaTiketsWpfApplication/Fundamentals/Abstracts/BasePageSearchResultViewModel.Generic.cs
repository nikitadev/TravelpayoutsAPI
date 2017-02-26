using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace AviaTicketsWpfApplication.Fundamentals.Abstracts
{
    public abstract class BasePageSearchResultViewModel<T> : BasePageViewModel, ICollectionViewModel<T>
    {
        private ObservableCollection<T> _collection;
        public ObservableCollection<T> Collection
        {
            get { return _collection; }
            set { Set(ref _collection, value); }
        }

        protected BasePageSearchResultViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();
            
            MessengerInstance.Register<ISearchQuery>(this, MessengerTokens.Search, async (query) => await SearchQueryHandler(query));
        }

        protected void SendError(string error)
        {
            MessengerInstance.Send(new SearchResultMessage { Message = error });
        }

        protected async Task SearchQueryHandler(ISearchQuery searchQuery)
        {
            if (searchQuery == null)
                return;

            Collection = null;
            IsVisibleData = false;
            IsPageMessageVisible = false;
            IsProgress = true;

            var list = await UpdateCollection(searchQuery);
            if (list != null)
            {
                Collection = new ObservableCollection<T>(list);
            }

            IsVisibleData = Collection != null && Collection.Any();
            IsPageMessageVisible = !IsVisibleData;
            IsProgress = false;

            MessengerInstance.Send(new SearchResultMessage { IsFinished = true });          
        }

        /// <summary>
        /// Add to update collection here
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        protected abstract Task<IEnumerable<T>> UpdateCollection(ISearchQuery searchQuery);
    }
}
