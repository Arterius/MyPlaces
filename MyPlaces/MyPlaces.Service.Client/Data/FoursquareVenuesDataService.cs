using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Model;
using MyPlaces.Service.Client.Contracts.Repository;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.Service.Client.DTO.Foursquare;
using MyPlaces.Service.Client.Data.Helper;

namespace MyPlaces.Service.Client.Data
{
    public class FoursquareVenuesDataService : IPlacesDataService
    {
        private readonly IPlacesRepository<RootObject> _placesRepository;
        private const string _baseUri = "https://api.foursquare.com/v2/venues/explore";
        private readonly string _clientId;
        private readonly string _clientSecret;

        public FoursquareVenuesDataService(IPlacesRepository<RootObject> placesRepository, string clientId, string clientSecret)
        {
            _placesRepository = placesRepository;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public async Task<List<Place>> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentNullException(nameof(keyword));

            try
            {
                IUriBuilder uriBuilder = new FoursquareVenueUriBuilder(_baseUri, _clientId, _clientSecret, keyword);
                RootObject response = await _placesRepository.GetPlaces(uriBuilder.Construct());

                if (response.Meta.Code != 200)
                {
                    throw new Exception("HTTP response is not OK");
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}