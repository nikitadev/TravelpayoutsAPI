using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library;

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
        protected readonly IApiFactory _apiFactory;

        protected readonly Lazy<Task<Tuple<string, string>>> _apiInfo;

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

            _apiInfo = new Lazy<Task<Tuple<string, string>>>(() => _cacheService.GetApiInfoAsync(), true);
        }

        /// <summary>
        /// Initializes a new instance of the BasePageViewModel class.
        /// </summary>
        public BaseViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : this(cacheService)
        {
            _apiFactory = apiFactory;
        }

        protected abstract Task InitializeAsync();

        protected virtual async Task ClearAsync()
        {
            await Task.Yield();

            Cleanup();
        }
    }
}