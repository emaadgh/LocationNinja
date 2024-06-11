namespace LocationNinja.Features.IpLocation;

public sealed record IpLocationDetailedResponse(
    string Ip,
    double Latitude,
    double Longitude,
    string Country,
    string Region,
    string City);

