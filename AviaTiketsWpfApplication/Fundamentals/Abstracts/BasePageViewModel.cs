using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Properties;
using System.Threading.Tasks;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using TravelpayoutsAPI.Library;

namespace AviaTicketsWpfApplication.Fundamentals.Abstracts
{
    public abstract class BasePageViewModel : BaseViewModel
    {
        private bool _isProgress;
        public bool IsProgress
        {
            get { return _isProgress; }
            set { Set(ref _isProgress, value); }
        }

        private bool _isPageMessageVisible;
        public bool IsPageMessageVisible
        {
            get { return _isPageMessageVisible; }
            set { Set(ref _isPageMessageVisible, value); }
        }
        
        private string _pageMessage;
        public string PageMessage
        {
            get { return _pageMessage; }
            set { Set(ref _pageMessage, value); }
        }

        private bool _isVisibleData;
        public bool IsVisibleData
        {
            get { return _isVisibleData; }
            set { Set(ref _isVisibleData, value); }
        }

        protected BasePageViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
            IsProgress = false;
            IsVisibleData = false;
            IsPageMessageVisible = true;
            PageMessage = Resources.NotData;
        }

        protected override async Task InitializeAsync()
        {
            await Task.Yield();
        }
    }
}
