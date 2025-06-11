using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;
using Microsoft.Extensions.Caching.Memory;

namespace ThreadPilot.Infrastructure.Services;

public class VehicleServiceClient : IVehicleServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<VehicleServiceClient> _logger;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly IMemoryCache _cache;

    public VehicleServiceClient(HttpClient httpClient, ILogger<VehicleServiceClient> logger, IConfiguration configuration, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
        _cache = cache;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<VehicleInfoDto> GetVehicleAsync(string registrationNumber, CancellationToken cancellationToken)
    {
        string cacheKey = $"VehicleInfo_{registrationNumber}";
        if (_cache.TryGetValue<VehicleInfoDto>(cacheKey, out var cachedVehicle))
        {
            return cachedVehicle;
        }
        try
        {
            var response = await _httpClient.GetAsync($"/api/vehicles/{Uri.EscapeDataString(registrationNumber)}", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Vehicle not found for registration number: {RegistrationNumber}", registrationNumber);
                return null;
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var vehicle = JsonSerializer.Deserialize<VehicleInfoDto>(content, _jsonOptions);

            if (vehicle != null)
            {
                _cache.Set(cacheKey, vehicle, TimeSpan.FromHours(24));
            }
            return vehicle;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error calling Vehicle Service for registration number: {RegistrationNumber}", registrationNumber);
            throw new InvalidOperationException($"Failed to get vehicle information for registration number {registrationNumber}", ex);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex, "Request to Vehicle Service timed out for registration number: {RegistrationNumber}", registrationNumber);
            throw new InvalidOperationException($"Request timed out while getting vehicle information for registration number {registrationNumber}", ex);
        }
    }
}