namespace LocationNinja.IntegrationMessages.Internal;
public sealed record ResolvedLocationMessage(
    string Country,
    string Region,
    string City);
