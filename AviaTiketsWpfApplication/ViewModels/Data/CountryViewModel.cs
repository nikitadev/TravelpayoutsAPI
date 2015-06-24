using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using GalaSoft.MvvmLight;
using TravelpayoutsAPI.Library.Models.Data;

namespace AviaTicketsWpfApplication.ViewModels.Data
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CountryViewModel : BaseDataViewModel<Country>
    {
        /// <summary>
        /// Initializes a new instance of the CountryViewModel class.
        /// </summary>
        public CountryViewModel(Country c)
            : base(c)
        {
        }
    }
}