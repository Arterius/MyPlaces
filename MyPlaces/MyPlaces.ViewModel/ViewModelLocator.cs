using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Ioc;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.Service.Client.Service;
using MyPlaces.Service.Client.Repository;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.DTO.Google;
using MyPlaces.Service.Client.Contracts.Service.General;

namespace MyPlaces.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();

            SimpleIoc.Default.Register<IHttpService, HttpService>();
            SimpleIoc.Default.Register<IPlacesRepository<RootObject>, GooglePlacesRepository>();
            SimpleIoc.Default.Register<IPlacesDataService>(() => new GooglePlacesDataService
            (
                SimpleIoc.Default.GetInstance<IPlacesRepository<RootObject>>(),
                "AIzaSyDI3Q6N_PIKL3yW_HR2OApUhFFR-BbIzxs"
            ));
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}
