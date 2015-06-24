using GalaSoft.MvvmLight;
using System;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public sealed class MonthViewModel : ViewModelBase
    {
        private readonly Action<MonthViewModel> _actionUpdate;
        public int Number { get; private set; }

        public string Name { get; set; }

        private bool _isChecked;
        public bool IsChecked 
        {
            get { return _isChecked; }
            set 
            { 
                Set(ref _isChecked, value);

                _actionUpdate(this);
            }
        }

        public MonthViewModel(int number, Action<MonthViewModel> actionUpdate)
        {
            Number = number;
            _actionUpdate = actionUpdate;
        }
    }
}