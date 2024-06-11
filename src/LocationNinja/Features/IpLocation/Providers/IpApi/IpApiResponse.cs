using System.Text.Json.Serialization;

namespace LocationNinja.Features.IpLocation.Providers.IpApi
{
    public class IpApiResponse
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("country")]
        public required string Country { get; set; }

        [JsonPropertyName("regionName")]
        public required string Region { get; set; }

        [JsonPropertyName("city")]
        public required string City { get; set; }
    }
}
