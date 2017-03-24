using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Model;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.Service.Client.DTO.Foursquare;
using MyPlaces.Service.Client.Service.Helper;
using MyPlaces.Service.Client.Exceptions;

namespace MyPlaces.Service.Client.Service
{
    public class FoursquareVenuesDataService : IPlacesDataService
    {
        private readonly IPlacesRepository<RootObject> _placesRepository;
        private readonly IUriBuilder _uriBuilder;
        private const string _baseUri = "https://api.foursquare.com/v2/venues/explore";
        private const int _limit = 20;
        private int _offset = 1;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private string _keyword;

        public FoursquareVenuesDataService(IPlacesRepository<RootObject> placesRepository, string clientId, string clientSecret)
        {
            if (placesRepository == null) throw new ArgumentNullException(nameof(placesRepository));
            if (string.IsNullOrWhiteSpace(clientId)) throw new ArgumentNullException(nameof(clientId));
            if (string.IsNullOrWhiteSpace(clientSecret)) throw new ArgumentNullException(nameof(clientSecret));

            _placesRepository = placesRepository;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _uriBuilder = new FoursquareVenueUriBuilder(_baseUri, _clientId, _clientSecret, _limit.ToString());
        }

        public async Task<List<Place>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) throw new ArgumentNullException(nameof(keyword));

            _keyword = keyword;

            return await MakeRequest(_uriBuilder.ConstructSearch(keyword));
        }

        public async Task<List<Place>> GetNext()
        {
            string offset = (_limit * _offset).ToString();
            _offset += 1;
            return await MakeRequest(_uriBuilder.ConstructGetNext(_keyword, offset));
        }

        private async Task<List<Place>> MakeRequest(Uri requestUri)
        {
            try
            {
                RootObject response = await _placesRepository.GetPlaces(requestUri);

                if (response.Meta.Code != 200)
                {
                    throw new DataAccessException("Error Getting Data");
                }

                List<Venue> venues = response.Response.Groups.SelectMany(g => g.Items.Select(i => i.Venue)).ToList();
                List<Place> places = venues.Select(v => new Place
                {
                    Address = v.Location.Address,
                    Name = v.Name,
                    Rating = v.Rating
                }).ToList();

                return places;
            }
            catch (BaseException)
            {
                throw;
            }
        }
    }
}