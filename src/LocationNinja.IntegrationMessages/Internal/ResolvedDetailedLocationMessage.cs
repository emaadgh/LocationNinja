namespace LocationNinja.IntegrationMessages.Internal;
public sealed record ResolvedDetailedLocationMessage(
    double Latitude,
    double Longitude,
    string Country,
    string Region,
    string City);
