using Newtonsoft.Json;
using System.Collections.Generic;

namespace MyPlaces.Service.Client.DTO.Google
{
    public class Result
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "formatted_address")]
        public string FormattedAddress { get; set; }

        public List<Photo> Photos { get; set; }

        public double Rating { get; set; }
    }
}
