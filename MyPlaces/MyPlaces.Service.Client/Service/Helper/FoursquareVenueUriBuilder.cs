using System;
using System.Net;

namespace MyPlaces.Service.Client.Service.Helper
{
    public class FoursquareVenueUriBuilder : IUriBuilder
    {
        private readonly string _baseUri;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _keyword;

        public FoursquareVenueUriBuilder(string baseUri, string clientId, string clientSecret, string keyword)
        {
            _baseUri = baseUri;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _keyword = keyword;
        }

        public Uri Construct()
        {
            string result = $"{_baseUri}?client_id={_clientId}&client_secret={_clientSecret}&v=20130815&near=Yerevan&query={WebUtility.HtmlEncode(_keyword)}";

            return new Uri(result);
        }
    }
}
