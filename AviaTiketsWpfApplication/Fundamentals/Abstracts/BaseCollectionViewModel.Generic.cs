using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace AviaTicketsWpfApplication.Fundamentals.Abstracts
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public abstract class BaseCollectionViewModel<T> : BasePageViewModel, ICollectionViewModel<T>
    {
        private ObservableCollection<T> _collection;
        public ObservableCollection<T> Collection
        {
            get { return _collection; }
            private set { Set(ref _collection, value); }
        }

        public BaseCollectionViewModel(ISearchTicketApiFactory searchTicketApiFactory, ICacheService cacheService)
            : base(searchTicketApiFactory, cacheService)
        {
            PageMessage = Resources.DataLoading;
        }

        protected sealed override async Task InitializeAsync()
        {
            var message = new ViewModelMessage { IsShowingProgress = true };
            MessengerInstance.Send(message);

            IsVisibleData = false;

            var list = await UpdateCollection();
            Collection = new ObservableCollection<T>(list);

            IsVisibleData = Collection != null && Collection.Any(); ;
            IsPageMessageVisible = !IsVisibleData;

            message.IsShowingProgress = false;
            MessengerInstance.Send(message);
        }

        /// <summary>
        /// Add to update collection here
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        protected abstract Task<IEnumerable<T>> UpdateCollection();
    }
}