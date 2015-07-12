using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Navigation;
using AviaTicketsWpfApplication.Fundamentals.Abstracts;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using AviaTicketsWpfApplication.Models;
using AviaTicketsWpfApplication.Properties;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        private string _titlePrev;

        public RelayCommand AboutCommand { get; private set; }
        public RelayCommand GotoGithubCommand { get; set; }
        public RelayCommand<NavigatingCancelEventArgs> NavigatingCommand { get; set; }
        public RelayCommand<NavigationEventArgs> NavigatedCommand { get; set; }

        private bool _isShowSearchButtonEnabled;
        public bool IsShowSearchButtonEnabled
        {
            get { return _isShowSearchButtonEnabled; }
            set { Set(ref _isShowSearchButtonEnabled, value); }
        }
		
		private bool _isProgressVisible;
        public bool IsProgressVisible 
        {
            get { return _isProgressVisible; }
            set { Set(ref _isProgressVisible, value); }
        }

        private bool _isMenuVisible;
        public bool IsMenuVisible 
        {
            get { return _isMenuVisible; }
            set { Set(ref _isMenuVisible, value); }
        }

        private bool _isFrameVisible;
        public bool IsFrameVisible 
        {
            get { return _isFrameVisible; }
            set { Set(ref _isFrameVisible, value); }
        }

        private bool _isFormOpen;
        public bool IsFlyoutOpen
        {
            get { return _isFormOpen; }
            set { Set(ref _isFormOpen, value); }
        }

        private bool _isShortSearchFormOpen;
        public bool IsShortSearchFlyoutOpen
        {
            get { return _isShortSearchFormOpen; }
            set { Set(ref _isShortSearchFormOpen, value); }
        }

        private bool _isCommandBarOpen;
        public bool IsCommandBarOpen
        {
            get { return _isCommandBarOpen; }
            set { Set(ref _isCommandBarOpen, value); }
        }

        private string _titleFirstColumn;
        public string TitleFirstColumn
        {
            get { return _titleFirstColumn; }
            set { Set(ref _titleFirstColumn, value); }
        }

        private string _titleSecondColumn;
        public string TitleSecondColumn
        {
            get { return _titleSecondColumn; }
            set { Set(ref _titleSecondColumn, value); }
        }

        private string _pageTitle;
        public string PageTitle
        {
            get { return _pageTitle; }
            set { Set(ref _pageTitle, value); }
        }

        private ObservableCollection<TileViewModel> _searchTiles;
        public ObservableCollection<TileViewModel> SearchTiles 
        {
            get { return _searchTiles; }
            set { Set(ref _searchTiles, value); }
        }

        private ObservableCollection<TileViewModel> _infoTiles;
        public ObservableCollection<TileViewModel> InfoTiles
        {
            get { return _infoTiles; }
            set { Set(ref _infoTiles, value); }
        }
        
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(ICacheService cacheService)
            : base(cacheService)
        {
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
			}
			else
			{
                IsMenuVisible = true;
                IsFrameVisible = false;
                IsProgressVisible = false;
                IsFlyoutOpen = false;
                IsCommandBarOpen = false;
                IsShowSearchButtonEnabled = true;

                TitleFirstColumn = Resources.TitleFirstBlock;
                TitleSecondColumn = Resources.TitleSecondBlock;

                AboutCommand = new RelayCommand(AboutCommandHandler);
                NavigatingCommand = new RelayCommand<NavigatingCancelEventArgs>(NavigatingCommandHandler);
                NavigatedCommand = new RelayCommand<NavigationEventArgs>(NavigatedCommandHandler);

                MessengerInstance.Register<ViewModelMessage>(this, async (m) => await ViewModelMessageHandler(m));
			}
        }

        protected override async Task InitializeAsync()
        {
            await DispatcherHelper.RunAsync(() =>
            {
                SearchTiles = new ObservableCollection<TileViewModel>();

                SearchTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.SlateBlue),
                    Title = Resources.CheapestTickets,
                    SizeMode = TileSizeMode.Medium,
                    IconName = Settings.Default.ChipTicketsIconName,
                    TypePage = typeof(CheapTicketsViewModel)
                });

                SearchTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.DarkCyan),
                    Title = Resources.DirectFlights,
                    SizeMode = TileSizeMode.Medium,
                    IconName = Settings.Default.DirectTicketsIconName,
                    TypePage = typeof(DirectTicketsViewModel)
                });

                SearchTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.MediumSeaGreen),
                    Title = Resources.TicketsGroupByMonth,
                    SizeMode = TileSizeMode.Small,
                    IconName = Settings.Default.CalendarTicketsIconName,
                    TypePage = typeof(CalendarTicketsViewModel)
                });

                SearchTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.IndianRed),
                    Title = Resources.SpecialOffers,
                    SizeMode = TileSizeMode.Large,
                    IconName = Settings.Default.SpecialOfferIconName,
                    TypePage = typeof(SpecialOffersViewModel)
                });

                InfoTiles = new ObservableCollection<TileViewModel>();

                InfoTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.DarkGoldenrod),
                    Title = Resources.Cities,
                    SizeMode = TileSizeMode.Medium,
                    IconName = Settings.Default.CitiesIconName,
                    TypePage = typeof(CityDirectionsViewModel)
                });

                InfoTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.DarkViolet),
                    Title = Resources.Countries,
                    SizeMode = TileSizeMode.Small,
                    IconName = Settings.Default.CountriesIconName,
                    TypePage = typeof(CountryListViewModel)
                });

                InfoTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.Salmon),
                    Title = Resources.Airports,
                    SizeMode = TileSizeMode.Small,
                    IconName = Settings.Default.AirportsIconName,
                    TypePage = typeof(AirportListViewModel)
                });

                InfoTiles.Add(new TileViewModel
                {
                    Background = new SolidColorBrush(Colors.DimGray),
                    Title = Resources.Airlines,
                    SizeMode = TileSizeMode.Medium,
                    IconName = Settings.Default.AirlineIconName,
                    TypePage = typeof(AirlineDirectionsViewModel)
                });
            });
        }

        private void NavigatingCommandHandler(NavigatingCancelEventArgs args)
        {
            if (args.NavigationMode == NavigationMode.New)
            {
                IsFrameVisible = true;
                IsMenuVisible = false;
            } 
            else if (args.NavigationMode == NavigationMode.Back)
            {
                if (args.Uri == null)
                {
                    IsMenuVisible = true;
                    IsFrameVisible = false;
                    IsShowSearchButtonEnabled = false;
                }

                IsFlyoutOpen = false;
                PageTitle = _titlePrev;
            }
        }

        private void NavigatedCommandHandler(NavigationEventArgs e)
        {
            if (e.ExtraData != null)
            {
                string parametrs = e.ExtraData.ToString();

                var dicParams = parametrs.Split('&').Select(s =>
                {
                    var keyvalue = s.Split('=');

                    return new Tuple<string, string>(keyvalue[0], keyvalue[1]);
                }).ToDictionary(x => x.Item1, x => x.Item2);

                string title;
                if (dicParams.TryGetValue("title", out title))
                {
                    _titlePrev = PageTitle;

                    PageTitle = title;

                    dicParams.Remove("title");
                }

                if (dicParams.Count > 0)
                {
                    MessengerInstance.Send(new DetailsPageMessage { Parametrs = dicParams });
                }
            }
        }

        private void AboutCommandHandler()
        {
            MessengerInstance.Send(new DialogMessage
            {
                DlgType = DialogType.About,
                ActType = ActionType.Show,
                DialogTemplateKey = "AboutDialogKey"
            });
        }

        private async Task ViewModelMessageHandler(ViewModelMessage message)
        {
            if (message.IsInitialized)
            {
                await CallEnterTokenDialogAsync();
            }

            IsProgressVisible = message.IsShowingProgress;
            IsFlyoutOpen = message.IsShowedSearhPanel;
            IsShowSearchButtonEnabled = message.IsSearhEnabled;
        }

        public async Task CallEnterTokenDialogAsync()
        {
            string token = await _token.Value;
            if (!String.IsNullOrEmpty(token))
                return;

            MessengerInstance.Send(new DialogMessage
            {
                DlgType = DialogType.Login,
                ActType = ActionType.Show,
                DialogTemplateKey = "TokenDialogKey"
            });
        }
    }
}