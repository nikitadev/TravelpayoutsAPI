using GalaSoft.MvvmLight;
using System;
using TravelpayoutsAPI.Library.Models.Monitor;

namespace AviaTicketsWpfApplication.ViewModels.Data
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OfferViewModel : ViewModelBase
    {
        private string _title;
        public string Title 
        {
            get { return _title; }
            set { Set(ref _title, value); }
        }

        private Uri _link;
        public Uri Link
        {
            get { return _link; }
            set { Set(ref _link, value); }
        }

        /// <summary>
        /// Initializes a new instance of the OfferViewModel class.
        /// </summary>
        public OfferViewModel(Offer offer)
        {
            Title = offer.Title;
            Link = new Uri(offer.Link);
        }
    }
}