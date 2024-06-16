using LocationNinja.Features.IpLocation.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LocationNinja.Features.IpLocation.Endpoints;

public static class Endpoints
{
    public static IEndpointRouteBuilder MapIpLocationFeatureEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        endpointRouteBuilder.MapGet("/locations/{ip_address:required}",
                                    async ([FromRoute(Name = "ip_address")] string ipAddress,
                                    IIpLocationService ipLocationService,
                                    CancellationToken cancellationToken) =>
                                    {
                                        return await ipLocationService.GetLocation(ipAddress, cancellationToken);
                                    }).Validator<string>()
                                      .WithTags(EndpointSchema.LocationTag);

        endpointRouteBuilder.MapGet("/locations/{ip_address:required}/detailed",
                                    async ([FromRoute(Name = "ip_address")] string ipAddress,
                                    IIpLocationService ipLocationService,
                                    CancellationToken cancellationToken) =>
                                    {
                                        return await ipLocationService.GetDetailedLocation(ipAddress, cancellationToken);
                                    }).Validator<string>()
                                      .WithTags(EndpointSchema.LocationTag); ;

        return endpointRouteBuilder;
    }
}
