using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Model;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.Service.Client.DTO.Google;
using MyPlaces.Service.Client.Service.Helper;

namespace MyPlaces.Service.Client.Service
{
    public class GooglePlacesDataService : IPlacesDataService
    {
        private readonly IPlacesRepository<RootObject> _placesRepository;
        private const string _baseUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
        private readonly string _apiKey;

        public GooglePlacesDataService(IPlacesRepository<RootObject> placesRepository, string apiKey)
        {
            _placesRepository = placesRepository;
            _apiKey = apiKey;
        }

        public async Task<List<Place>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentNullException(nameof(keyword));

            try
            {
                IUriBuilder uriBuilder = new GooglePlaceUriBuilder(_baseUri, _apiKey, keyword);
                RootObject response = await _placesRepository.GetPlaces(uriBuilder.Construct());

                if (response.Status != "OK")
                {
                    throw new Exception("HTTP response is not OK");
                }

                List<Place> places = response.Results.Select(p => new Place
                {
                    Address = p.FormattedAddress,
                    Name = p.Name,
                    Rating = p.Rating
                }).ToList();

                return places;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
