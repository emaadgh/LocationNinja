namespace LocationNinja.Features.IpLocation.Models;

public sealed record IpLocationProviderResponse(
    double Latitude,
    double Longitude,
    string Country,
    string Region,
    string City);
