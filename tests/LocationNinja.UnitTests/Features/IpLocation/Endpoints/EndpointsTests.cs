using System.Net;
using System.Net.Http.Json;
using AutoMapper;
using FluentAssertions;
using LocationNinja.Common.Persistence;
using LocationNinja.Features.IpLocation;
using LocationNinja.Features.IpLocation.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace LocationNinja.UnitTests.Features.IpLocation.Endpoints;

public class IpLocationEndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    private readonly string _validIp = "11.11.11.11";
    private readonly string _invalidIp = "invalid ip";
    private readonly double _latitude = 12.34;
    private readonly double _longitude = 56.78;
    private readonly string _country = "test country";
    private readonly string _region = "test region";
    private readonly string _city = "test city";

    public IpLocationEndpointsTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureTestServices(services =>
            {
                var ipLocationServiceMock = new Mock<IIpLocationService>();

                ipLocationServiceMock.Setup(service => service.GetLocation(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new IpLocationResponse(
                        _country,
                        _region,
                        _city));

                ipLocationServiceMock.Setup(service => service.GetDetailedLocation(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(new IpLocationDetailedResponse(
                        _latitude,
                        _longitude,
                        _country,
                        _region,
                        _city));

                services.AddSingleton(ipLocationServiceMock.Object);
            });
        });
    }

    [Fact]
    public async Task GetLocation_ShouldReturnBadRequest_WhenIpAddressIsInvalid()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/locations/{_invalidIp}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetLocation_ShouldReturnOk_WhenIpAddressIsValid()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/locations/{_validIp}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<IpLocationResponse>();
        content.Should().NotBeNull();
        content?.Country.Should().Be(_country);
        content?.Region.Should().Be(_region);
        content?.City.Should().Be(_city);
    }

    [Fact]
    public async Task GetDetailedLocation_ShouldReturnBadRequest_WhenIpAddressIsInvalid()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/locations/{_invalidIp}/detailed");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetDetailedLocation_ShouldReturnOk_WhenIpAddressIsValid()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync($"/locations/{_validIp}/detailed");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<IpLocationDetailedResponse>();
        content.Should().NotBeNull();
        content?.Latitude.Should().Be(_latitude);
        content?.Longitude.Should().Be(_longitude);
        content?.Country.Should().Be(_country);
        content?.Region.Should().Be(_region);
        content?.City.Should().Be(_city);
    }
}
