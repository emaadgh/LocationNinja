namespace LocationNinja.Features.IpLocation;

public static class DependencyInjections
{
    public static void AddIpLocationFeaature(this WebApplicationBuilder builder)
    {
        var settings = builder.Configuration.Get<AppSettings>();
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        builder.Services.AddScoped<IIpLocationService, IpLocationService>();

        builder.Services.AddHttpClient<ILocationAPI, IpApiProvider>(options =>
        {
            options.BaseAddress = new Uri(settings.Features.IpLocation.IpApiBaseUrl);
        }).SetHandlerLifetime(Timeout.InfiniteTimeSpan);
    }
}
