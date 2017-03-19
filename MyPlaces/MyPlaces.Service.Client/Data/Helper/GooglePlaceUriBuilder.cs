using System;

namespace MyPlaces.Service.Client.Data.Helper
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
            //TODO: Encode {keyWord} to valid searchable string

            string result = $"{_baseUri}?query={_keyword}&key={_apiKey}";

            return new Uri(result);
        }
    }
}
