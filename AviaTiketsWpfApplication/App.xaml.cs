using System.Windows;
using AviaTicketsWpfApplication.Views;
using GalaSoft.MvvmLight.Threading;

namespace AviaTicketsWpfApplication
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
    {
        static App()
        {
            DispatcherHelper.Initialize();
        }

		protected override async void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var splashScreen = new SplashScreenView();
			splashScreen.Show();

			await splashScreen.LoadDataAsync();

			var mainView = new MainWindowView();
			mainView.Show();

			splashScreen.Close();
		}
	}
}
