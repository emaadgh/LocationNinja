namespace LocationNinja.Features.IpLocation;

public interface IIpLocationService
{
    Task<IpLocationResponse> GetLocation(string ip, CancellationToken cancellationToken = default);
    Task<IpLocationDetailedResponse> GetDetailedLocation(string ip, CancellationToken cancellationToken = default);
}