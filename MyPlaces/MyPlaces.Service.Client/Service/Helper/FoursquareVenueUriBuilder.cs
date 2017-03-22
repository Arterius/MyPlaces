using System;
using System.Net;

namespace MyPlaces.Service.Client.Service.Helper
{
    public class FoursquareVenueUriBuilder : IUriBuilder
    {
        private readonly string _baseUri;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public FoursquareVenueUriBuilder(string baseUri, string clientId, string clientSecret)
        {
            _baseUri = baseUri;
            _clientId = clientId;
            _clientSecret = clientSecret;
        }

        public Uri ConstructSearch(string keyword)
        {
            string uri = $"{_baseUri}?client_id={_clientId}&client_secret={_clientSecret}&v=20130815&near=Yerevan&query={WebUtility.HtmlEncode(keyword)}";
            return new Uri(uri);
        }

        public Uri ConstructGetNext(string param)
        {
            throw new NotImplementedException();
        }
    }
}
