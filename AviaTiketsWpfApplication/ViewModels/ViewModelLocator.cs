/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:AviaTicketsWpfApplication"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using TravelpayoutsAPI.Library.Infostructures.Implements;
using TravelpayoutsAPI.Library.Infostructures.Interfaces;
using AviaTicketsWpfApplication.Fundamentals;
using AviaTicketsWpfApplication.Models;
using System.IO;
using TravelpayoutsAPI.Library.Infostructures;
using AviaTicketsWpfApplication.Fundamentals.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using TravelpayoutsAPI.Library;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AviaTicketsWpfApplication.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        public CityDirectionsViewModel CityDirections
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CityDirectionsViewModel>();
            }
        }

        public AirlineDirectionsViewModel AirlineDirections
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AirlineDirectionsViewModel>();
            }
        }

        public AirportListViewModel AirportList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AirportListViewModel>();
            }
        }

        public CountryListViewModel CountryList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CountryListViewModel>();
            }
        }

        public CalendarTicketsViewModel CalendarTikets
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CalendarTicketsViewModel>();
            }
        }

        public SpecialOffersViewModel SpecialOffers
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SpecialOffersViewModel>();
            }
        }

        public CheapTicketsViewModel CheapTickets
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CheapTicketsViewModel>();
            }
        }

        public DirectTicketsViewModel DirectTikets
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DirectTicketsViewModel>();
            }
        }

        public RealtimeTicketsViewModel RealtimeTickets
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RealtimeTicketsViewModel>();
            }
        }

        public object TokenDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TokenDialogViewModel>();
            }
        }

        public object AboutDialog
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutDialogViewModel>();
            }
        }

        public SplashScreenViewModel SplashScreen
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SplashScreenViewModel>();
            }
        }

        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>(Guid.NewGuid().ToString());
            }
        }

        public SimpleSearchViewModel SimpleSearch
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SimpleSearchViewModel>(Guid.NewGuid().ToString());
            }
        }

        public SearchByMonthViewModel SearchByMonth
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchByMonthViewModel>(Guid.NewGuid().ToString());
            }
        }

        public RealtimeSearchViewModel RealtimeSearch
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RealtimeSearchViewModel>(Guid.NewGuid().ToString());
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

			if (ViewModelBase.IsInDesignModeStatic)
			{
				// Create design time view services and models
				//SimpleIoc.Default.Register<IDataService, DesignDataService>();
			}
			else
			{
				var connection = new DbConnection("data");

                SimpleIoc.Default.Register<IDbConnection>(() => connection);
				SimpleIoc.Default.Register<IRepository<CacheItem>, CacheRepository>();
                SimpleIoc.Default.Register<ICacheService, CacheService>();

                SimpleIoc.Default.Register<IApiFactory, ApiFactory>();

				SimpleIoc.Default.Register<Bootstrapper>();

                // pages
                SimpleIoc.Default.Register<CalendarTicketsViewModel>();
                SimpleIoc.Default.Register<SpecialOffersViewModel>();
                SimpleIoc.Default.Register<CheapTicketsViewModel>();
                SimpleIoc.Default.Register<DirectTicketsViewModel>();
                SimpleIoc.Default.Register<CountryListViewModel>();
                SimpleIoc.Default.Register<AirportListViewModel>();
                SimpleIoc.Default.Register<CityDirectionsViewModel>();
                SimpleIoc.Default.Register<AirlineDirectionsViewModel>();
                SimpleIoc.Default.Register<RealtimeTicketsViewModel>();

                // search forms
                SimpleIoc.Default.Register<SearchViewModel>();
                SimpleIoc.Default.Register<SimpleSearchViewModel>();
                SimpleIoc.Default.Register<SearchByMonthViewModel>();
                SimpleIoc.Default.Register<RealtimeSearchViewModel>();

                // dialogs
                SimpleIoc.Default.Register<TokenDialogViewModel>();
                SimpleIoc.Default.Register<AboutDialogViewModel>();

				SimpleIoc.Default.Register<SplashScreenViewModel>();
				SimpleIoc.Default.Register<MainViewModel>();
			}
		}

        public static Uri GetPathPage(Type typeViewModel)
        {
            if (typeViewModel == null)
                return null;

            string name = typeViewModel.Name.Replace("Model", String.Empty);

            string path = Path.Combine("Pages", Path.ChangeExtension(name, "xaml"));

            return new Uri(String.Concat("pack://application:,,,/Views/", path));
        }

        public static object GetInstance(Type typeViewModel)
        {
            if (typeViewModel == null)
                return null;

            string typeViewName = typeViewModel.FullName.Replace("Model", String.Empty);

            var typeView = Type.GetType(typeViewName);
            if (typeView == null)
                return null;

            return Activator.CreateInstance(typeView);
        }

        public static async Task ClearAsync()
        {
            var service = ServiceLocator.Current.GetInstance<ICacheService>();
            await service.ClearFromTemporaryAsync();
        }

        public static void Cleanup()
        {
            Messenger.Reset();
        }
    }
}