namespace LocationNinja.Features.IpLocation.Models;

public sealed record IpLocationDetailedResponse(
    double Latitude,
    double Longitude,
    string Country,
    string Region,
    string City);

