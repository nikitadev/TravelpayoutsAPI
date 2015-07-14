using AviaTicketsWpfApplication.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Deployment.Application;
using System.Reflection;

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

        public string Version { get; private set; }

        /// <summary>
        /// Initializes a new instance of the AboutDialogViewModel class.
        /// </summary>
        public AboutDialogViewModel()
        {
            AcceptCommand = new RelayCommand(() => AcceptCommandHandler());

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                Version = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
            {
                var assem = Assembly.GetEntryAssembly();
                var assemName = assem.GetName();

                Version = assemName.Version.ToString();
            }
        }

        private void AcceptCommandHandler()
        {
            MessengerInstance.Send(new DialogMessage { DlgType = DialogType.About, ActType = ActionType.Hide });
        }
    }
}