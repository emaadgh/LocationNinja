using LocationNinja.Common;
using LocationNinja.Features.IpLocation.Model;
using Microsoft.Extensions.Options;

namespace LocationNinja.Features.IpLocation.Providers.IpApi;

public class IpApiProvider(HttpClient httpClient) : ILocationAPI
{
    public async Task<IpLocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken = default)
    {
        var url = $"?query={ip}";

        var response = await httpClient.GetFromJsonAsync<IpApiResponse>(url, cancellationToken);

        if (response is null)
        {
            throw new Exception("httpResponse is null");
        }

        return new IpLocationApiResponse(response.Latitude,
                                      response.Longitude,
                                      response.Country,
                                      response.Region,
                                      response.City);
    }
}
