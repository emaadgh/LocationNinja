using AutoMapper;
using LocationNinja.Common.Persistence;
using LocationNinja.Features.IpLocation.Domain;
using LocationNinja.Features.IpLocation.Models;
using Microsoft.EntityFrameworkCore;

namespace LocationNinja.Features.IpLocation;

public class IpLocationService(ILocationAPI locationAPI,
                               IMapper mapper,
                               LocationNinjaDbContext locationNinjaDbContext)
{
    private readonly ILocationAPI _locationAPI = locationAPI;
    private readonly IMapper _mapper = mapper;
    private readonly LocationNinjaDbContext _locationNinjaDbContext = locationNinjaDbContext;


    public async Task<IpLocationResponse> GetLocation(string ip, CancellationToken cancellationToken = default)
    {
        Location? locationFromDB = await FetchLocationFromDB(ip, cancellationToken);

        if (locationFromDB is not null)
        {
            var ipLocationResponseFromDB = _mapper.Map<IpLocationResponse>(locationFromDB);

            return ipLocationResponseFromDB;
        }

        IpLocationApiResponse locationFromAPI = await FetchLocationFromApi(ip, cancellationToken);

        var ipLocationResponseFromAPI = _mapper.Map<IpLocationResponse>(locationFromAPI);

        return ipLocationResponseFromAPI;
    }

    public async Task<IpLocationDetailedResponse> GetDetailedLocation(string ip, CancellationToken cancellationToken)
    {
        Location? locationFromDB = await FetchLocationFromDB(ip, cancellationToken);

        if (locationFromDB is not null)
        {
            var ipLocationResponseFromDB = new IpLocationDetailedResponse(
                ip,
                locationFromDB.Latitude,
                locationFromDB.Longitude,
                locationFromDB.Country,
                locationFromDB.Region,
                locationFromDB.City);

            return ipLocationResponseFromDB;
        }

        IpLocationApiResponse locationFromAPI = await FetchLocationFromApi(ip, cancellationToken);

        var ipLocationResponseFromAPI = new IpLocationDetailedResponse(
                ip,
                locationFromAPI.Latitude,
                locationFromAPI.Longitude,
                locationFromAPI.Country,
                locationFromAPI.Region,
                locationFromAPI.City);

        return ipLocationResponseFromAPI;
    }

    private async Task<Location?> FetchLocationFromDB(string ip, CancellationToken cancellationToken)
    {
        return await _locationNinjaDbContext.Locations.FirstOrDefaultAsync(l => l.Ip == ip, cancellationToken);
    }

    private async Task<IpLocationApiResponse> FetchLocationFromApi(string ip, CancellationToken cancellationToken)
    {
        var locationFromAPI = await _locationAPI.GetAsync(ip, cancellationToken);

        var locationToPersist = _mapper.Map<Location>(locationFromAPI);
        locationToPersist.Ip = ip;

        await _locationNinjaDbContext.Locations.AddAsync(locationToPersist, cancellationToken);
        await _locationNinjaDbContext.SaveChangesAsync(cancellationToken);

        return locationFromAPI;
    }
}