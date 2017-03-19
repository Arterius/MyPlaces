using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyPlaces.Service.Client.DTO.Google
{
    public class RootObject
    {
        [JsonProperty(PropertyName = "next_page_token")]
        public string NextPageToken { get; set; }
        public List<Result> Results { get; set; }
        public string Status { get; set; }
    }
}
