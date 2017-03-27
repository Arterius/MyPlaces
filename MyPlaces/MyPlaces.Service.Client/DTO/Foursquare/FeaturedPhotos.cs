using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyPlaces.Service.Client.DTO.Foursquare
{
    public class FeaturedPhotos
    {
        public int Count { get; set; }

        public List<Item2> Items { get; set; }
    }

    //[JsonObject(Title = "Items")]
    public class Item2
    {
        public string Id { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
