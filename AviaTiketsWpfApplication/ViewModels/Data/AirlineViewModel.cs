using AviaTicketsWpfApplication.Fundamentals;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.ViewModels.Data
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AirlineViewModel : BaseDataViewModel<Airline>
    {
        protected override Type TypeDetailedViewModel
        {
            get { return typeof(AirlineDirectionsViewModel); }
        }

        /// <summary>
        /// Initializes a new instance of the AirlineViewModel class.
        /// </summary>
        public AirlineViewModel(Airline airline)
            : base(airline)
        {
        }
    }
}