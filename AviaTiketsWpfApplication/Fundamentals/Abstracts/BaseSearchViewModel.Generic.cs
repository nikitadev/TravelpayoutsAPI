using System;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using GalaSoft.MvvmLight.Command;
using TravelpayoutsAPI.Library;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;

namespace AviaTicketsWpfApplication.Fundamentals.Abstracts
{
    public abstract class BaseSearchViewModel<T> : BaseViewModel, IValidateViewModel where T : ISearchQuery, new()
    {
        protected T _searchQuery;

        public RelayCommand SearchCommand { get; private set; }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { Set(ref _message, value); }
        }

        private bool _isMessageVisible;
        public bool IsMessageVisible
        {
            get { return _isMessageVisible; }
            set { Set(ref _isMessageVisible, value); }
        }

        private bool _isSearching;
        public bool IsSearching
        {
            get { return _isSearching; }
            set { Set(ref _isSearching, value); }
        }
        
        protected BaseSearchViewModel()
            : base()
        {
            _searchQuery = new T();

            Initialize();
        }

        protected BaseSearchViewModel(IApiFactory apiFactory, ICacheService cacheService)
            : base(apiFactory, cacheService)
        {
            _searchQuery = new T();

            Initialize();
        }

        private void Initialize()
        {
            IsSearching = false;
            IsMessageVisible = false;

            SearchCommand = new RelayCommand(SearchCommandHandler, GetValidate);
        }

        /// <summary>
        /// Must call in override method
        /// </summary>
        /// <returns></returns>
        protected override async Task InitializeAsync()
        {
            Message = String.Empty;
            IsMessageVisible = false;
            IsSearching = false;

            MessengerInstance.Register<SearchResultMessage>(this, async (m) => await OnSearchResultHandler(m));
            
            await Task.Yield();
        }

        /// <summary>
        /// Must call from begin in body override method
        /// </summary>
        private void SearchCommandHandler()
        {
            OnSearchCommandHandler();

            MessengerInstance.Send<ISearchQuery>(_searchQuery, MessengerTokens.Search);
        }

        protected virtual void OnSearchCommandHandler()
        {
            IsSearching = true;
            SearchCommand.RaiseCanExecuteChanged();

            IsMessageVisible = false;
        }

        protected virtual async Task OnSearchResultHandler(SearchResultMessage message)
        {
            if (message.IsFinished)
            {
                IsSearching = false;
                SearchCommand.RaiseCanExecuteChanged();
            }
            else
            {
                Message = message.Message;
                IsMessageVisible = !String.IsNullOrEmpty(message.Message);
            }

            await Task.Yield();
        }

        public virtual bool GetValidate()
        {
            return !IsSearching;
        }
    }
}
