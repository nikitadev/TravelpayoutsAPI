using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
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
    public abstract class BaseViewModel : ViewModelBase
    {
        protected readonly ICacheService _cacheService;
        protected readonly ISearchTicketApiFactory _searchTicketApiFactory;

        protected readonly Lazy<Task<string>> _token;

        public RelayCommand ContentLoadedCommand { get; set; }
        public RelayCommand ContentUnloadedCommand { get; set; }

        protected BaseViewModel()
        {
            ContentLoadedCommand = new RelayCommand(async () => await InitializeAsync());
            ContentUnloadedCommand = new RelayCommand(async () => await ClearAsync());
        }

        public BaseViewModel(ICacheService cacheService)
            : this()
        {
            _cacheService = cacheService;

            _token = new Lazy<Task<string>>(() => _cacheService.GetTokenAsync(), true);
        }

        /// <summary>
        /// Initializes a new instance of the BasePageViewModel class.
        /// </summary>
        public BaseViewModel(ISearchTicketApiFactory searchTicketApiFactory, ICacheService cacheService)
            : this(cacheService)
        {
            _searchTicketApiFactory = searchTicketApiFactory;
        }

        protected abstract Task InitializeAsync();

        protected virtual async Task ClearAsync()
        {
            await Task.Yield();

            Cleanup();
        }
    }
}