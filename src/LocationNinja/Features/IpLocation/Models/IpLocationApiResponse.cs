namespace LocationNinja.Features.IpLocation.Models;

public sealed record IpLocationApiResponse(
    double Latitude,
    double Longitude,
    string Country,
    string Region,
    string City);
