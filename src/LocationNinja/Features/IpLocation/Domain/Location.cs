using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace LocationNinja.Features.IpLocation.Domain;

[Collection("Locations")]
public class Location
{
    public ObjectId Id { get; set; }
    public required string Ip { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public required string Country { get; set; }
    public required string Region { get; set; }
    public required string City { get; set; }
};
