using AutoMapper;
using FluentAssertions;
using LocationNinja.Features.IpLocation.Domain;
using LocationNinja.Features.IpLocation.Models;
using LocationNinja.Features.IpLocation.Providers.IpApi;
using LocationNinja.Features.IpLocation;
using LocationNinja.Features.IpLocation.Profiles;

namespace LocationNinja.UnitTests.Features.Profiles;

public class LocationProfileTests
{
    private readonly IMapper _mapper;

    private readonly string _ip = "11.11.11.11";
    private readonly double _latitude = 12.34;
    private readonly double _longitude = 56.78;
    private readonly string _country = "test country";
    private readonly string _region = "test region";
    private readonly string _city = "test city";

    public LocationProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<LocationProfile>();
        });

        _mapper = config.CreateMapper();
    }

    [Fact]
    public void Configuration_ShouldBeValid()
    {
        // Assert
        _mapper.ConfigurationProvider.AssertConfigurationIsValid();
    }

    [Fact]
    public void Map_LocationToIpLocationResponse_ShouldSucceed()
    {
        // Arrange
        var location = new Location
        {
            Ip = _ip,
            Country = _country,
            Region = _region,
            City = _city
        };

        // Act
        var response = _mapper.Map<IpLocationResponse>(location);

        // Assert
        response.Should().BeEquivalentTo(new IpLocationResponse(
            _country,
            _region,
            _city));
    }

    [Fact]
    public void Map_LocationToIpLocationDetailedResponse_ShouldSucceed()
    {
        // Arrange
        var location = new Location
        {
            Ip = _ip,
            Latitude = _latitude,
            Longitude = _longitude,
            Country = _country,
            Region = _region,
            City = _city
        };

        // Act
        var response = _mapper.Map<IpLocationDetailedResponse>(location);

        // Assert
        response.Should().BeEquivalentTo(new IpLocationDetailedResponse(
            _latitude,
            _longitude,
            _country,
            _region,
            _city));
    }

    [Fact]
    public void Map_IpLocationApiResponseToLocation_ShouldSucceed()
    {
        // Arrange
        var apiResponse = new IpLocationApiResponse(
            _latitude,
            _longitude,
            _country,
            _region,
            _city);

        // Act
        var location = _mapper.Map<Location>(apiResponse);
        location.Ip = _ip;

        // Assert
        location.Should().BeEquivalentTo(new Location
        {
            Ip = _ip,
            Latitude = _latitude,
            Longitude = _longitude,
            Country = _country,
            Region = _region,
            City = _city
        });
    }

    [Fact]
    public void Map_IpLocationApiResponseToIpLocationResponse_ShouldSucceed()
    {
        // Arrange
        var apiResponse = new IpLocationApiResponse(
            _latitude,
            _longitude,
            _country,
            _region,
            _city);

        // Act
        var response = _mapper.Map<IpLocationResponse>(apiResponse);

        // Assert
        response.Should().BeEquivalentTo(new IpLocationResponse(
            _country,
            _region,
            _city));
    }

    [Fact]
    public void Map_IpLocationApiResponseToIpLocationDetailedResponse_ShouldSucceed()
    {
        // Arrange
        var apiResponse = new IpLocationApiResponse(
            _latitude,
            _longitude,
            _country,
            _region,
            _city);

        // Act
        var response = _mapper.Map<IpLocationDetailedResponse>(apiResponse);

        // Assert
        response.Should().BeEquivalentTo(new IpLocationDetailedResponse(
            _latitude,
            _longitude,
            _country,
            _region,
            _city));
    }

    [Fact]
    public void Map_IpApiResponseToIpLocationApiResponse_ShouldSucceed()
    {
        // Arrange
        var apiResponse = new IpApiResponse
        {
            Latitude = _latitude,
            Longitude = _longitude,
            Country = _country,
            Region = _region,
            City = _city
        };

        // Act
        var response = _mapper.Map<IpLocationApiResponse>(apiResponse);

        // Assert
        response.Should().BeEquivalentTo(new IpLocationApiResponse(
            _latitude,
            _longitude,
            _country,
            _region,
            _city));
    }
}
