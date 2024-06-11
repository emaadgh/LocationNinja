namespace LocationNinja.Features.IpLocation;

public sealed record IpLocationResponse(
    string Country,
    string Region,
    string City);
