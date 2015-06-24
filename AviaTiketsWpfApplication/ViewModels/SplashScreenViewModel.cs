using GalaSoft.MvvmLight;
using System;
using System.Threading.Tasks;
using AviaTicketsWpfApplication.Fundamentals;
using AviaTicketsWpfApplication.Properties;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;

namespace AviaTicketsWpfApplication.ViewModels
{
	public sealed class SplashScreenViewModel : ViewModelBase, IProgress<Tuple<int, string>>
	{
		private readonly Bootstrapper _bootstrapper;

		private string _result;
		public string Result
		{
			get { return _result; }
			set	{ Set(ref _result, value); }
		}

		private int _progress;
		public int Progress
		{
			get { return _progress; }
			set	{ Set(ref _progress, value); }
		}

		public SplashScreenViewModel(Bootstrapper bootstrapper)
		{
			_bootstrapper = bootstrapper;
		}

		public async Task LoadDataAsync()
		{
			await _bootstrapper.InitializeAsync(this);
        }

		public void Report(Tuple<int, string> value)
		{
			Progress = value.Item1;
            Result = String.Format(Resources.FormatLoadingLine, value.Item1, value.Item2);
		}
    }
}