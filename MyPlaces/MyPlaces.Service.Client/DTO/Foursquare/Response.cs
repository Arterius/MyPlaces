using System.Collections.Generic;

namespace MyPlaces.Service.Client.DTO.Foursquare
{
    public class Response
    {
        public int TotalResults { get; set; }

        public List<Group> Groups { get; set; }
    }
}
