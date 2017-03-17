using System.Collections.Generic;

namespace MyPlaces.Service.Client.DTO.Google
{
    public class RootObject
    {
        public string NextPageToken { get; set; }
        public List<Result> Results { get; set; }
        public string Status { get; set; }
    }
}
