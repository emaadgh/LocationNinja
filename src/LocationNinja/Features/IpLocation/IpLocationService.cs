namespace LocationNinja.Features.IpLocation;

public class IpLocationService(ILocationProvider locationAPI,
                               IMapper mapper,
                               LocationNinjaDbContext locationNinjaDbContext) : IIpLocationService
{
    private readonly ILocationProvider _locationApi = locationAPI;
    private readonly IMapper _mapper = mapper;
    private readonly LocationNinjaDbContext _locationNinjaDbContext = locationNinjaDbContext;

    public async Task<IpLocationResponse> GetLocation(string ip, CancellationToken cancellationToken = default)
    {
        Location? locationFromDB = await FetchLocationFromDB(ip, cancellationToken);

        if (locationFromDB is not null)
        {
            return _mapper.Map<IpLocationResponse>(locationFromDB);
        }

        IpLocationProviderResponse locationFromAPI = await FetchLocationFromApi(ip, cancellationToken);

        return _mapper.Map<IpLocationResponse>(locationFromAPI);
    }

    public async Task<IpLocationDetailedResponse> GetDetailedLocation(string ip, CancellationToken cancellationToken = default)
    {
        Location? locationFromDB = await FetchLocationFromDB(ip, cancellationToken);

        if (locationFromDB is not null)
        {
            return _mapper.Map<IpLocationDetailedResponse>(locationFromDB);
        }

        IpLocationProviderResponse locationFromAPI = await FetchLocationFromApi(ip, cancellationToken);

        return _mapper.Map<IpLocationDetailedResponse>(locationFromAPI);
    }

    private async Task<Location?> FetchLocationFromDB(string ip, CancellationToken cancellationToken)
    {
        return await _locationNinjaDbContext.Locations.FirstOrDefaultAsync(l => l.Ip == ip, cancellationToken);
    }

    private async Task<IpLocationProviderResponse> FetchLocationFromApi(string ip, CancellationToken cancellationToken)
    {
        var locationFromAPI = await _locationApi.GetAsync(ip, cancellationToken);

        var locationToPersist = _mapper.Map<Location>(locationFromAPI);
        locationToPersist.Ip = ip;

        await _locationNinjaDbContext.Locations.AddAsync(locationToPersist, cancellationToken);
        await _locationNinjaDbContext.SaveChangesAsync(cancellationToken);

        return locationFromAPI;
    }
}