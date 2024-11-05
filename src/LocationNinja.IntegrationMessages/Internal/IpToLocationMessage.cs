namespace LocationNinja.IntegrationMessages.Internal;
public sealed record IpToLocationMessage(
    Guid RequestId,
    string Ip,
    LocationType LocationType);
