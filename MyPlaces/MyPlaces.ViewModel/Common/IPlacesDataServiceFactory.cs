using MyPlaces.Service.Client.Contracts.Service.Data;

namespace MyPlaces.ViewModel.Common
{
    public interface IPlacesDataServiceFactory
    {
        IPlacesDataService GetDataService(string key);
    }
}
