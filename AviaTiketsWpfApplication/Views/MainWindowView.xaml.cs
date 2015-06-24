using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace AviaTicketsWpfApplication.Views
{
	/// <summary>
	/// Interaction logic for MainWindowView.xaml
	/// </summary>
	public partial class MainWindowView : MetroWindow
	{
        private bool _shutdown;
        private BaseMetroDialog _currentDialog;

        public MainWindowView()
        {
            InitializeComponent();

            Closing += (s, e) => { e.Cancel = !_shutdown; DialogMessageHandleAsync(new DialogMessage { DlgType = DialogType.Close }); };

            mainFrame.NavigationService.Navigate(DataContext);
        }

        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);

            Messenger.Default.Register<PageMessage>(this, PageMessageHandler);
            Messenger.Default.Register<FlyoutMessage>(this, FlyoutMessageHandler);
            Messenger.Default.Register<DialogMessage>(this, async (m) => await DialogMessageHandleAsync(m));

            Messenger.Default.Send<ViewModelMessage>(new ViewModelMessage { IsInitialized = true });
        }

        private void FlyoutMessageHandler(FlyoutMessage m)
        {
            flyoutContent.Content = null;

            flyout.Header = m.Header;
            flyout.Position = m.IsRightPosition ? Position.Right : Position.Left;
            flyoutContent.Content = ViewModelLocator.GetInstance(m.TypeViewModel);
        }

        private void PageMessageHandler(PageMessage m)
        {
            var uriPage = ViewModelLocator.GetPathPage(m.TypeViewModel);
            mainFrame.NavigationService.Navigate(uriPage, m.Parametrs);
        }

        private async Task DialogMessageHandleAsync(DialogMessage obj)
        {
            if (obj.ActType == ActionType.Close)
            {
                Application.Current.Shutdown();
            }
            else
            {
                switch (obj.DlgType)
                {
                    case DialogType.Login:
                    case DialogType.About:
                        if (obj.ActType == ActionType.Show)
                        {
                            var viewModelLocator = (ViewModelLocator)App.Current.Resources["Locator"];
                            if (viewModelLocator == null)
                                return;

                            _currentDialog = (BaseMetroDialog)App.Current.Resources[obj.DialogTemplateKey];
                            _currentDialog.DataContext = obj.DlgType == DialogType.Login
                                ? viewModelLocator.TokenDialog 
                                : viewModelLocator.AboutDialog;

                            await this.ShowMetroDialogAsync(_currentDialog);
                        }
                        else if (_currentDialog != null)
                        {
                            await this.HideMetroDialogAsync(_currentDialog);
                        }

                        break;
                    case DialogType.Close:
                        
                        if (_shutdown) return;

                        var settings = new MetroDialogSettings()
                        {
                            AffirmativeButtonText = Properties.Resources.Close,
                            NegativeButtonText = Properties.Resources.Cancel,
                            AnimateShow = true,
                            AnimateHide = false
                        };

                        var result = await this.ShowMessageAsync(
                            Properties.Resources.TitleDlgExitApp,
                            Properties.Resources.QuestionExitApp,
                            MessageDialogStyle.AffirmativeAndNegative, settings);

                        if (result == MessageDialogResult.Affirmative)
                        {
                            await ViewModelLocator.ClearAsync();

                            ViewModelLocator.Cleanup();

                            _shutdown = true;
                            Application.Current.Shutdown();
                        }

                        break;
                    case DialogType.Notification:
                        await this.ShowMessageAsync("Notification", "Test");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
