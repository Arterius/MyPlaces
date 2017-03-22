using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.Service.Client.Service;
using MyPlaces.Service.Client.Repository;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.Contracts.Service.General;
using MyPlaces.ViewModel.Service;
using GoogleDto = MyPlaces.Service.Client.DTO.Google;
using FoursquareDto = MyPlaces.Service.Client.DTO.Foursquare;

namespace MyPlaces.ViewModel
{
    public class ViewModelLocator
    {
        public const string MainPage = "MainPage";
        public const string SettingsPage = "SettingsPage";

        public NavigationService NavigationService { get; private set; }

        public ViewModelLocator()
        {
            NavigationService = new NavigationService();

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();

            SimpleIoc.Default.Register<INavigationService>(() => NavigationService);
            SimpleIoc.Default.Register<IHttpService, HttpService>();

            //SimpleIoc.Default.Register<IPlacesRepository<RootObject>, GooglePlacesRepository>();
            //SimpleIoc.Default.Register<IPlacesDataService>(() => new GooglePlacesDataService
            //(
            //    SimpleIoc.Default.GetInstance<IPlacesRepository<RootObject>>(),
            //    "AIzaSyDI3Q6N_PIKL3yW_HR2OApUhFFR-BbIzxs"
            //));

            SimpleIoc.Default.Register<IPlacesRepository<FoursquareDto.RootObject>, FoursquareVenuesRepository>();
            SimpleIoc.Default.Register<IPlacesDataService>(() => new FoursquareVenuesDataService
            (
                SimpleIoc.Default.GetInstance<IPlacesRepository<FoursquareDto.RootObject>>(),
                "UZH4KD340XSEKTU1WPUJT0EKNSF1QCOH00EUTQGOASKWIRUB",
                "ZL1NJN13P53C4OSHHMTPSHZSCDPBDOWWBMAISERPRUGAVVP4"
            ));
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public SettingsViewModel Settings
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SettingsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
