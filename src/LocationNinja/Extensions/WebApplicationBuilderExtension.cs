using System.Reflection;

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

        builder.Services.AddMassTransit(configure =>
        {
            configure.AddConsumers(Assembly.GetExecutingAssembly());
            configure.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(settings.BrokerOptions.Host, hostConfigure =>
                {
                    hostConfigure.Username(settings.BrokerOptions.Username);
                    hostConfigure.Password(settings.BrokerOptions.Password);
                });

                cfg.UseRawJsonSerializer();

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}
