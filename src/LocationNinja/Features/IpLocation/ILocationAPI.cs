using LocationNinja.Features.IpLocation.Models;

namespace LocationNinja.Features.IpLocation;

public interface ILocationAPI
{
    Task<IpLocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken = default);
}
