using System;
using System.Net;

namespace MyPlaces.Service.Client.Service.Helper
{
    public class GooglePlaceUriBuilder : IUriBuilder
    {
        private readonly string _baseUri;
        private readonly string _apiKey;

        public GooglePlaceUriBuilder(string baseUri, string apiKey)
        {
            _baseUri = baseUri;
            _apiKey = apiKey;
        }

        public Uri ConstructSearch(string keyword)
        {
            string uri = $"{_baseUri}?query={WebUtility.HtmlEncode(keyword)}&key={_apiKey}";
            return new Uri(uri);
        }

        public Uri ConstructGetNext(string param)
        {
            string uri = $"{_baseUri}?pagetoken={param}&key={_apiKey}";
            return new Uri(uri);
        }
    }
}
