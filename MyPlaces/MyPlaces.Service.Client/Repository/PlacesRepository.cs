using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Model;
using MyPlaces.Service.Client.Contracts.Service.General;
using MyPlaces.Service.Client.Contracts.Repository;

namespace MyPlaces.Service.Client.Repository
{
    public class PlacesRepository : BaseRepository, IPlacesRepository
    {
        public PlacesRepository(IHttpService httpService) : base(httpService) { }

        public async Task<List<Place>> GetPlaces(Uri requestUri)
        {
            return await GetAsync<List<Place>>(requestUri);
        }
    }
}
