using AviaTicketsWpfApplication.Fundamentals;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using GalaSoft.MvvmLight;
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
    public class CityViewModel : BaseDataViewModel<City>
    {
        protected override Type TypeDetailedViewModel
        {
            get { return typeof(CityDirectionsViewModel); }
        }

        /// <summary>
        /// Initializes a new instance of the CityViewModel class.
        /// </summary>
        public CityViewModel(City c)
            : base(c)
        {
        }
    }
}