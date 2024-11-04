namespace LocationNinja.Features.IpLocation;

public interface ILocationProvider
{
    Task<IpLocationProviderResponse> GetAsync(string ip, CancellationToken cancellationToken = default);
}
