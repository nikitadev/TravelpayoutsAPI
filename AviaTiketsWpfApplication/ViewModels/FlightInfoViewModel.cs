using System;
using GalaSoft.MvvmLight;
using TravelpayoutsAPI.Library.Models.Search;

namespace AviaTicketsWpfApplication.ViewModels
{
    public sealed class FlightInfoViewModel : ViewModelBase
    {
        public TimeSpan DepartTime { get; set; }

        public DateTime DepartDate { get; set; }

        public TimeSpan ReturnTime { get; set; }

        public DateTime ReturnDate { get; set; }

        public FlightInfoViewModel(Proposal proposal)
        {
            //DepartTime = new TimeSpan(pro)
        }
    }
}
