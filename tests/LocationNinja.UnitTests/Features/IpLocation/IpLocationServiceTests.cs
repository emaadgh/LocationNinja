using AutoMapper;
using FluentAssertions;
using LocationNinja.Common.Persistence;
using LocationNinja.Features.IpLocation;
using LocationNinja.Features.IpLocation.Domain;
using LocationNinja.Features.IpLocation.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace LocationNinja.UnitTests.Features.IpLocation;

public class IpLocationServiceTests
{
    private readonly Mock<ILocationProvider> _locationApiMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly string _ip = "11.11.11.11";
    private readonly IpLocationApiResponse _apiResponse;
    private readonly Location _location;
    private readonly IpLocationResponse _expectedShortResponse;
    private readonly IpLocationDetailedResponse _expectedDetailedResponse;

    public IpLocationServiceTests()
    {
        _locationApiMock = new Mock<ILocationProvider>();
        _mapperMock = new Mock<IMapper>();

        double latitude = 12.34;
        double longitude = 56.78;
        string country = "test country";
        string region = "test region";
        string city = "test city";

        _apiResponse = new IpLocationApiResponse(
            latitude,
            longitude,
            country,
            region,
            city);

        _location = new Location
        {
            Ip = _ip,
            Country = country,
            Region = region,
            City = city
        };

        _expectedShortResponse = new IpLocationResponse(country, region, city);
        _expectedDetailedResponse = new IpLocationDetailedResponse(latitude, longitude, country, region, city);
    }

    [Fact]
    public async Task GetLocation_ShouldReturnLocationFromDb_WhenLocationExistsInDb()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var ipLocationService = new IpLocationService(_locationApiMock.Object, _mapperMock.Object, dbContext);

        dbContext.Locations.Add(_location);
        await dbContext.SaveChangesAsync();

        _mapperMock.Setup(m => m.Map<IpLocationResponse>(It.IsAny<Location>())).Returns(_expectedShortResponse);

        // Act
        var result = await ipLocationService.GetLocation(_ip);

        // Assert
        result.Should().Be(_expectedShortResponse);

        _locationApiMock.Verify(api => api.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetLocation_ShouldReturnLocationFromApi_WhenLocationDoesNotExistInDb()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var ipLocationService = new IpLocationService(_locationApiMock.Object, _mapperMock.Object, dbContext);

        _locationApiMock.Setup(api => api.GetAsync(_ip, It.IsAny<CancellationToken>())).ReturnsAsync(_apiResponse);
        _mapperMock.Setup(m => m.Map<IpLocationResponse>(_apiResponse)).Returns(_expectedShortResponse);
        _mapperMock.Setup(m => m.Map<Location>(_apiResponse)).Returns(_location);


        // Act
        var result = await ipLocationService.GetLocation(_ip);

        // Assert
        result.Should().Be(_expectedShortResponse);

        _locationApiMock.Verify(api => api.GetAsync(_ip, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetDetailedLocation_ShouldReturnLocationFromDb_WhenLocationExistsInDb()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var ipLocationService = new IpLocationService(_locationApiMock.Object, _mapperMock.Object, dbContext);

        dbContext.Locations.Add(_location);
        await dbContext.SaveChangesAsync();

        _mapperMock.Setup(m => m.Map<IpLocationDetailedResponse>(It.IsAny<Location>())).Returns(_expectedDetailedResponse);

        // Act
        var result = await ipLocationService.GetDetailedLocation(_ip, CancellationToken.None);

        // Assert
        result.Should().Be(_expectedDetailedResponse);

        _locationApiMock.Verify(api => api.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task GetDetailedLocation_ShouldReturnLocationFromApi_WhenLocationDoesNotExistInDb()
    {
        // Arrange
        var dbContext = CreateDbContext();
        var ipLocationService = new IpLocationService(_locationApiMock.Object, _mapperMock.Object, dbContext);

        _locationApiMock.Setup(api => api.GetAsync(_ip, It.IsAny<CancellationToken>())).ReturnsAsync(_apiResponse);
        _mapperMock.Setup(m => m.Map<IpLocationDetailedResponse>(_apiResponse)).Returns(_expectedDetailedResponse);
        _mapperMock.Setup(m => m.Map<Location>(_apiResponse)).Returns(_location);

        // Act
        var result = await ipLocationService.GetDetailedLocation(_ip, CancellationToken.None);

        // Assert
        result.Should().Be(_expectedDetailedResponse);

        _locationApiMock.Verify(api => api.GetAsync(_ip, It.IsAny<CancellationToken>()), Times.Once);
    }

    private LocationNinjaDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<LocationNinjaDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new LocationNinjaDbContext(options);
    }
}