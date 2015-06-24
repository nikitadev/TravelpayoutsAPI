using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class SimpleSearchViewModel : BaseSearchViewModel<TextSearchQuery>
    {
        public string Watermark { get; set; }

        public string SearchText 
        {
            get { return _searchQuery.Text; }
            set { Set(ref _searchQuery.Text, value); } 
        }       

        private bool _isListVisible;
        public bool IsListVisible
        {
            get { return _isListVisible; }
            set { Set(ref _isListVisible, value); }
        }

        private List<IHyperlinkViewModel> _listResult;
        public List<IHyperlinkViewModel> ListResult 
        {
            get { return _listResult; }
            set { Set(ref _listResult, value); }
        }

        /// <summary>
        /// Initializes a new instance of the SimpleSearchViewModel class.
        /// </summary>
        public SimpleSearchViewModel()
            : base()
        {
            Watermark = "Search...";
        }

        protected override async Task InitializeAsync()
        {
            Clear();

            SearchText = String.Empty;

            await base.InitializeAsync();
        }

        protected override async Task OnSearchResultHandler(SearchResultMessage message)
        {
            await base.OnSearchResultHandler(message);
            
            if (message.ListResult != null && message.ListResult.Any())
            {
                await DispatcherHelper.RunAsync(() =>
                {
                    ListResult = message.ListResult.OrderBy(k => k.Title).ToList();
                    IsMessageVisible = true;
                    IsListVisible = true;
                });
            }
            else
            {
                Clear();
            }
        }

        protected override void OnSearchCommandHandler()
        {
            base.OnSearchCommandHandler();

            Clear();
        }

        private void Clear()
        {            
            IsListVisible = false;
            IsMessageVisible = false;
            ListResult = null;
        }
    }
}