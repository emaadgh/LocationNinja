namespace LocationNinja.Features.IpLocation;

public interface ILocationProvider
{
    Task<IpLocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken = default);
}
