namespace MyPlaces.Service.Client.DTO.Foursquare
{
    public class Venue
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public FeaturedPhotos FeaturedPhotos { get; set; }

        public Contact Contact { get; set; }

        public Location Location { get; set; }
    }
}
