using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPlaces.ViewModel.Common
{
    public class PlacesDataServiceProviders
    {
        public const string Google = "Google";
        public const string Foursquare = "Foursquare";

        public List<PlaceDataProvider> Providers { get; private set; }

        public PlacesDataServiceProviders()
        {
            Providers = new List<PlaceDataProvider>
            {
                new PlaceDataProvider { Id = Google, Name = "Google Places API" },
                new PlaceDataProvider { Id = Foursquare, Name = "Foursquare Venues API" }
            };
        }
    }

    public class PlaceDataProvider
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
