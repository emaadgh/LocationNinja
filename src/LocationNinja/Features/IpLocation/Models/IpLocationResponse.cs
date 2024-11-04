namespace LocationNinja.Features.IpLocation.Models;

public sealed record IpLocationResponse(
    string Country,
    string Region,
    string City);
