namespace LocationNinja.Subscribers;

public class IpToLocationMessageConsumer(IIpLocationService ipLocationService,
                                         IMapper mapper,
                                         ILogger<IpToLocationMessageConsumer> logger) : IConsumer<IpToLocationMessage>
{
    private readonly IIpLocationService _ipLocationService = ipLocationService;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<IpToLocationMessageConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<IpToLocationMessage> context)
    {
        try
        {
            var ipToLocationMessage = context.Message;

            if (ipToLocationMessage.LocationType == LocationType.Short)
            {
                var location = await _ipLocationService.GetLocation(context.Message.Ip, context.CancellationToken);

                if (location is null)
                {
                    _logger.LogWarning("Location not found for IP: {Ip}", context.Message.Ip);
                    return;
                }

                await context.Publish(_mapper.Map<ResolvedLocationMessage>(location), context.CancellationToken);

            }
            else
            {
                var location = await _ipLocationService.GetDetailedLocation(context.Message.Ip, context.CancellationToken);

                if (location is null)
                {
                    _logger.LogWarning("Detailed Location not found for IP: {Ip}", context.Message.Ip);
                    return;
                }

                await context.Publish(_mapper.Map<ResolvedDetailedLocationMessage>(location), context.CancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in IP to location consumer");
        }
    }
}
