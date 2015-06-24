using System;
using System.Threading.Tasks;
using MahApps.Metro.Controls;
using System.Windows;
using AviaTicketsWpfApplication.ViewModels;

namespace AviaTicketsWpfApplication.Views
{
	/// <summary>
	/// Interaction logic for SplashScreen.xaml
	/// </summary>
	public partial class SplashScreenView : Window
	{
		public SplashScreenView()
		{
			InitializeComponent();
		}

		internal async Task LoadDataAsync()
		{
			await (DataContext as SplashScreenViewModel).LoadDataAsync();
		}
	}
}
