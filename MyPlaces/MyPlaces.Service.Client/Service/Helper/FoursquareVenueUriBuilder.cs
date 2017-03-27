using System;
using System.Net;

namespace MyPlaces.Service.Client.Service.Helper
{
    public class FoursquareVenueUriBuilder : IUriBuilder
    {
        private readonly string _baseUri;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _limit;

        public FoursquareVenueUriBuilder(string baseUri, string clientId, string clientSecret, string limit)
        {
            _baseUri = baseUri;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _limit = limit;
        }

        public Uri ConstructSearch(string keyword)
        {
            string uri = $"{_baseUri}?client_id={_clientId}&client_secret={_clientSecret}&v=20130815&near=Yerevan&venuePhotos=1&query={WebUtility.HtmlEncode(keyword)}&limit={_limit}";
            return new Uri(uri);
        }

        public Uri ConstructGetNext(string keyword, string offset)
        {
            string uri = $"{_baseUri}?client_id={_clientId}&client_secret={_clientSecret}&v=20130815&near=Yerevan&venuePhotos=1&query={WebUtility.HtmlEncode(keyword)}&limit={_limit}&offset={offset}";
            return new Uri(uri);
        }
    }
}
