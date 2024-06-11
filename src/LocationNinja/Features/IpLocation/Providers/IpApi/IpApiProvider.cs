﻿using AutoMapper;
using LocationNinja.Common;
using LocationNinja.Features.IpLocation.Models;
using Microsoft.Extensions.Options;

namespace LocationNinja.Features.IpLocation.Providers.IpApi;

public class IpApiProvider(HttpClient httpClient, IMapper mapper) : ILocationAPI
{
    public async Task<IpLocationApiResponse> GetAsync(string ip, CancellationToken cancellationToken = default)
    {
        var url = $"?query={ip}";

        var response = await httpClient.GetFromJsonAsync<IpApiResponse>(url, cancellationToken);

        if (response is null)
        {
            throw new IpApiNullResponseException();
        }

        return mapper.Map<IpLocationApiResponse>(response);
    }
}
