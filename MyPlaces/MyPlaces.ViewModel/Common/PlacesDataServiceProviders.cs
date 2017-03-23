using System.Collections.Generic;
using System.Linq;

namespace MyPlaces.ViewModel.Common
{
    public class PlacesDataServiceProviders
    {
        private static PlacesDataServiceProviders _instance = null;
        private static readonly object _lock = new object();

        public const string Google = "Google";
        public const string Foursquare = "Foursquare";

        public static PlacesDataServiceProviders Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PlacesDataServiceProviders();
                    }
                    return _instance;
                }
            }
        }

        public List<PlaceDataProvider> Providers { get; private set; }
        public PlaceDataProvider Default { get; set; }

        private PlacesDataServiceProviders()
        {
            Providers = new List<PlaceDataProvider>
            {
                new PlaceDataProvider { Id = Google, Name = "Google Places API" },
                new PlaceDataProvider { Id = Foursquare, Name = "Foursquare Venues API" }
            };

            Default = Providers.First();
        }
    }

    public class PlaceDataProvider
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
