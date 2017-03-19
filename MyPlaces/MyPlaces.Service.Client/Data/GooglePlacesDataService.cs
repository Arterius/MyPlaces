using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Model;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.Service.Client.DTO.Google;

namespace MyPlaces.Service.Client.Data
{
    public class GooglePlacesDataService : IPlacesDataService
    {
        private readonly IPlacesRepository<RootObject> _placesRepository;
        private readonly string _apiKey;
        private const string _baseUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";

        public GooglePlacesDataService(IPlacesRepository<RootObject> placesRepository, string apiKey)
        {
            _placesRepository = placesRepository;
            _apiKey = apiKey;
        }

        public async Task<List<Place>> Search(string keyWord)
        {
            //TODO: Check for valid input parameters
            RootObject response = await _placesRepository.GetPlaces(ConstructUri(keyWord));

            return new List<Place>();
        }

        private Uri ConstructUri(string keyWord)
        {
            //TODO: Encode {keyWord} to valid searchable string
            string result = $"{_baseUri}?query={keyWord}&key={_apiKey}";

            return new Uri(result);
        }
    }
}
