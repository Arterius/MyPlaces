using Newtonsoft.Json;

namespace MyPlaces.Service.Client.DTO.Google
{
    public class Photo
    {
        public int Height { get; set; }

        public int Width { get; set; }

        [JsonProperty(PropertyName = "photo_reference")]
        public string PhotoReference { get; set; }
    }
}
