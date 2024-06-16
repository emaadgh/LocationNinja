using AutoMapper;
using LocationNinja.Features.IpLocation.Models;
using LocationNinja.Features.IpLocation.Providers.IpApi;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using LocationNinja.Features.IpLocation.Domain;

namespace LocationNinja.UnitTests.Features.Provider;

public class IpApiProviderTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly HttpClient _httpClient;
    private readonly IpApiProvider _ipApiProvider;

    public IpApiProviderTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        _mapperMock = new Mock<IMapper>();

        _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
        {
            BaseAddress = new Uri("https://api.test.com/")
        };

        _ipApiProvider = new IpApiProvider(_httpClient, _mapperMock.Object);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnIpLocationApiResponse_WhenApiResponseIsNotNull()
    {
        // Arrange
        var ip = "11.11.11.11";
        var apiResponse = new IpApiResponse
        {
            Latitude = 12.34,
            Longitude = 56.78,
            Country = "test country",
            Region = "test region",
            City = "test city"
        };

        var expectedResponse = new IpLocationApiResponse(
            12.34,
            56.78,
            "test country",
            "test region",
            "test city");

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = JsonContent.Create(apiResponse)
            });

        _mapperMock.Setup(m => m.Map<IpLocationApiResponse>(It.IsAny<IpApiResponse>()))
                   .Returns(expectedResponse);

        // Act
        var result = await _ipApiProvider.GetAsync(ip);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task GetAsync_ShouldThrowIpApiNullResponseException_WhenApiResponseIsNull()
    {
        // Arrange
        var ip = "11.11.11.11";

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("null") // Simulate null response
            });

        // Act
        var act = async () => await _ipApiProvider.GetAsync(ip);

        // Assert
        await act.Should().ThrowAsync<IpApiNullResponseException>();
    }
}
