using System.Net;
using System.Text.Json;
using FluentAssertions;
using ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;
using Xunit;

namespace ThreadPilot.API.IntegrationTests.VehicleService;

public class VehiclesControllerTests : IClassFixture<VehicleServiceWebApplicationFactory<ThreadPilot.VehicleService.API.Program>>
{
    private readonly HttpClient _client;
    private readonly VehicleServiceWebApplicationFactory<ThreadPilot.VehicleService.API.Program> _factory;
    private const string ApiKey = "vehicle-service-api-key-123456";

    public VehiclesControllerTests(VehicleServiceWebApplicationFactory<ThreadPilot.VehicleService.API.Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("X-API-Key", ApiKey);
    }

    [Fact]
    public async Task GetVehicle_ValidRegistrationNumber_ReturnsVehicle()
    {
        // Act
        var response = await _client.GetAsync("/api/vehicles/TST123");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var vehicle = JsonSerializer.Deserialize<VehicleDto>(content, options);

        vehicle.Should().NotBeNull();
        vehicle!.RegistrationNumber.Should().Be("TST123");
        vehicle.Color.Should().Be("Blue");
        vehicle.Model.Should().Be("Test Model 1");
    }

    [Fact]
    public async Task GetVehicle_NonExistentRegistrationNumber_ReturnsNotFound()
    {
        // Act
        var response = await _client.GetAsync("/api/vehicles/NONE12");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetVehicle_InvalidRegistrationNumberFormat_ReturnsBadRequest()
    {
        // Act
        var response = await _client.GetAsync("/api/vehicles/123");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetVehicle_NoApiKey_ReturnsUnauthorized()
    {
        // Arrange
        var clientWithoutApiKey = _factory.CreateClient();

        // Act
        var response = await clientWithoutApiKey.GetAsync("/api/vehicles/TST123");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetVehicle_InvalidApiKey_ReturnsUnauthorized()
    {
        // Arrange
        var clientWithInvalidApiKey = _factory.CreateClient();
        clientWithInvalidApiKey.DefaultRequestHeaders.Add("X-API-Key", "invalid-api-key");

        // Act
        var response = await clientWithInvalidApiKey.GetAsync("/api/vehicles/TST123");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}