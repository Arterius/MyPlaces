using System;
using System.Net;

namespace MyPlaces.Service.Client.Service.Helper
{
    public class GooglePlaceUriBuilder : IUriBuilder
    {
        private readonly string _baseUri;
        private readonly string _apiKey;
        private readonly string _keyword;

        public GooglePlaceUriBuilder(string baseUri, string apiKey, string keyword)
        {
            _baseUri = baseUri;
            _apiKey = apiKey;
            _keyword = keyword;
        }

        public Uri Construct()
        {
            string result = $"{_baseUri}?query={WebUtility.HtmlEncode(_keyword)}&key={_apiKey}";

            return new Uri(result);
        }
    }
}
