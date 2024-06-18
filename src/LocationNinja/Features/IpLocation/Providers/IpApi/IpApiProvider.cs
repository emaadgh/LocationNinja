using AutoMapper;
using LocationNinja.Features.IpLocation.Models;

namespace LocationNinja.Features.IpLocation.Providers.IpApi;

public class IpApiProvider(HttpClient httpClient, IMapper mapper) : ILocationAPI
{
    public async Task<IpLocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken = default)
    {

        var response = await httpClient.GetFromJsonAsync<IpApiResponse>(ip, cancellationToken);

        if (response is null)
        {
            throw new IpApiNullResponseException();
        }

        return mapper.Map<IpLocationApiResponse>(response);
    }
}
