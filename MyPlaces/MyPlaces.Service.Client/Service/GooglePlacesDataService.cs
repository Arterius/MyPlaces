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
        private readonly IUriBuilder _uriBuilder;
        private const string _baseUri = "https://maps.googleapis.com/maps/api/place/textsearch/json";
        private readonly string _apiKey;

        private string _nextPageToken = string.Empty;

        public GooglePlacesDataService(IPlacesRepository<RootObject> placesRepository, string apiKey)
        {
            if (placesRepository == null) throw new ArgumentNullException(nameof(placesRepository));
            if (string.IsNullOrWhiteSpace(apiKey)) throw new ArgumentNullException(nameof(apiKey));

            _placesRepository = placesRepository;
            _apiKey = apiKey;
            _uriBuilder = new GooglePlaceUriBuilder(_baseUri, _apiKey);
        }

        public async Task<List<Place>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) throw new ArgumentNullException(nameof(keyword));

            return await MakeRequest(_uriBuilder.ConstructSearch(keyword));
        }

        public async Task<List<Place>> GetNext()
        {
            if (string.IsNullOrEmpty(_nextPageToken))
            {
                throw new ArgumentOutOfRangeException(nameof(_nextPageToken));
            }
            return await MakeRequest(_uriBuilder.ConstructGetNext(_nextPageToken));
        }

        private async Task<List<Place>> MakeRequest(Uri requestUri)
        {
            try
            {
                RootObject response = await _placesRepository.GetPlaces(requestUri);

                if (response.Status != "OK")
                {
                    throw new Exception("HTTP response is not OK");
                }

                _nextPageToken = response.NextPageToken;

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
