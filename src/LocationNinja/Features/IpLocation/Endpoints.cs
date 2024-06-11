using Microsoft.AspNetCore.Mvc;

namespace LocationNinja.Features.IpLocation;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapIpLocationFeatureEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/locations/{ip_address:required}",
                                    async ([FromRoute(Name = "ip_address")] string ipAddress,
                                    IpLocationService ipLocationService,
                                    CancellationToken cancellationToken) =>
                                    {
                                        return await ipLocationService.GetLocation(ipAddress, cancellationToken);
                                    });

        endpointRouteBuilder.MapGet("/locations/{ip_address:required}/detailed",
                                    async ([FromRoute(Name = "ip_address")] string ipAddress,
                                    IpLocationService ipLocationService,
                                    CancellationToken cancellationToken) =>
                                    {
                                        return await ipLocationService.GetDetailedLocation(ipAddress, cancellationToken);
                                    });

        return endpointRouteBuilder;
    }
}
