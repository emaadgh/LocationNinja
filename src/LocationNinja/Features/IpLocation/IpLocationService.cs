using LocationNinja.Common.Persistence;
using LocationNinja.Features.IpLocation.Domain;
using LocationNinja.Features.IpLocation.Model;
using Microsoft.EntityFrameworkCore;

namespace LocationNinja.Features.IpLocation;

public class IpLocationService(ILocationAPI locationAPI,
                               LocationNinjaDbContext locationNinjaDbContext)
{
    private readonly ILocationAPI _locationAPI = locationAPI;
    private readonly LocationNinjaDbContext _locationNinjaDbContext = locationNinjaDbContext;

    public async Task<IpLocationResponse> GetLocation(string ip, CancellationToken cancellationToken = default)
    {
        Location? locationFromDB = await FetchLocationFromDB(ip, cancellationToken);

        if (locationFromDB is not null)
        {
            var ipLocationResponseFromDB = new IpLocationResponse(
                locationFromDB.Country,
                locationFromDB.Region,
                locationFromDB.City);

            return ipLocationResponseFromDB;
        }

        IpLocationApiResponse locationFromAPI = await FetchLocationFromApi(ip, cancellationToken);

        var ipLocationResponseFromAPI = new IpLocationResponse(
                locationFromAPI.Country,
                locationFromAPI.Region,
                locationFromAPI.City);

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

        var locationToPersist = new Location()
        {
            Ip = ip,
            Latitude = locationFromAPI.Latitude,
            Longitude = locationFromAPI.Longitude,
            Country = locationFromAPI.Country,
            Region = locationFromAPI.Region,
            City = locationFromAPI.City
        };


        await _locationNinjaDbContext.Locations.AddAsync(locationToPersist, cancellationToken);
        await _locationNinjaDbContext.SaveChangesAsync(cancellationToken);
        return locationFromAPI;
    }
}