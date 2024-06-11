using LocationNinja.Features.IpLocation.Model;

namespace LocationNinja.Features.IpLocation;

public interface ILocationAPI
{
    Task<IpLocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken = default);
}
