using System;
using System.Threading.Tasks;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.Contracts.Service.General;
using Google = MyPlaces.Service.Client.DTO.Google;

namespace MyPlaces.Service.Client.Repository
{
    public class GooglePlacesRepository : BaseRepository, IPlacesRepository<Google.RootObject>
    {
        public GooglePlacesRepository(IHttpService httpService) : base(httpService) { }

        public async Task<Google.RootObject> GetPlaces(Uri requestUri)
        {
            return await GetAsync<Google.RootObject>(requestUri);
        }
    }
}
