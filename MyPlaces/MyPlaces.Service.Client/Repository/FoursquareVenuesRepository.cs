using System;
using System.Threading.Tasks;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.Contracts.Service.General;
using Foursquare = MyPlaces.Service.Client.DTO.Foursquare;

namespace MyPlaces.Service.Client.Repository
{
    public class FoursquareVenuesRepository : BaseRepository, IPlacesRepository<Foursquare.RootObject>
    {
        public FoursquareVenuesRepository(IHttpService httpService) : base(httpService) { }

        public async Task<Foursquare.RootObject> GetPlaces(Uri requestUri)
        {
            return await GetAsync<Foursquare.RootObject>(requestUri);
        }
    }
}
