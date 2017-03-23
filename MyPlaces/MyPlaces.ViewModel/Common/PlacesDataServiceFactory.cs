using System.Collections.Generic;
using GalaSoft.MvvmLight.Ioc;
using MyPlaces.Service.Client.Contracts.Service.Data;

namespace MyPlaces.ViewModel.Common
{
    public class PlacesDataServiceFactory : IPlacesDataServiceFactory
    {
        private readonly Dictionary<string, IPlacesDataService> _dataServiceDictionary;

        public PlacesDataServiceFactory()
        {
            _dataServiceDictionary = new Dictionary<string, IPlacesDataService>
            {
                { PlacesDataServiceProviders.Foursquare, SimpleIoc.Default.GetInstance<IPlacesDataService>(PlacesDataServiceProviders.Foursquare) },
                { PlacesDataServiceProviders.Google, SimpleIoc.Default.GetInstance<IPlacesDataService>(PlacesDataServiceProviders.Google) }
            };
        }
        public IPlacesDataService GetDataService(string key)
        {

            if (_dataServiceDictionary.ContainsKey(key))
            {
                return _dataServiceDictionary[key];
            }

            throw new KeyNotFoundException();
        }
    }
}
