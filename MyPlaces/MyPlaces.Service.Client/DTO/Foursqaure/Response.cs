using System.Collections.Generic;

namespace MyPlaces.Service.Client.DTO.Foursqaure
{
    public class Response
    {
        public int TotalResults { get; set; }

        public List<Group> Groups { get; set; }
    }
}
