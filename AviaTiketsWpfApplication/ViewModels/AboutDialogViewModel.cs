using AviaTicketsWpfApplication.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AboutDialogViewModel : ViewModelBase
    {
        public RelayCommand AcceptCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the AboutDialogViewModel class.
        /// </summary>
        public AboutDialogViewModel()
        {
            AcceptCommand = new RelayCommand(() => AcceptCommandHandler());
        }

        private void AcceptCommandHandler()
        {
            MessengerInstance.Send(new DialogMessage { DlgType = DialogType.About, ActType = ActionType.Hide });
        }
    }
}