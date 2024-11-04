namespace LocationNinja.Extensions;

public static class WebApplicationBuilderExtension
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        var domainAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        builder.Services.Configure<AppSettings>(builder.Configuration);

        var settings = builder.Configuration.Get<AppSettings>();
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        builder.Services.AddDbContext<LocationNinjaDbContext>(options =>
        {
            options.UseMongoDB(settings.MongoDatabase.Host, settings.MongoDatabase.DatabaseName);
        });

        builder.Services.AddAutoMapper(domainAssemblies);

        builder.Services.AddValidatorsFromAssemblies(domainAssemblies);
    }
}
