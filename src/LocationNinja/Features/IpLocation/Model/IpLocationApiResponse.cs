using MongoDB.Bson;

namespace LocationNinja.Features.IpLocation.Model;

public sealed record IpLocationApiResponse(
    double Latitude,
    double Longitude,
    string Country,
    string Region,
    string City);
