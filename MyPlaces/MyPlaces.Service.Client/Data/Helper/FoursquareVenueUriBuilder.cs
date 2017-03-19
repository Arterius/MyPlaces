using System;

namespace MyPlaces.Service.Client.Data.Helper
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
            //TODO: Encode {keyWord} to valid searchable string
            string result = $"{_baseUri}?client_id={_clientId}&client_secret={_clientSecret}&v=20130815&near=Yerevan&query={_keyword}";

            return new Uri(result);
        }
    }
}
