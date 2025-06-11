using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

namespace ThreadPilot.Infrastructure.Services;

public class VehicleServiceClient : IVehicleServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<VehicleServiceClient> _logger;
    private readonly IConfiguration _configuration;
    private readonly JsonSerializerOptions _jsonOptions;

    public VehicleServiceClient(HttpClient httpClient, ILogger<VehicleServiceClient> logger, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _logger = logger;
        _configuration = configuration;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<VehicleInfoDto> GetVehicleAsync(string registrationNumber, CancellationToken cancellationToken)
    {
        try
        {
            var baseUrl = _configuration["VehicleService:BaseUrl"];
            var apiKey = _configuration["VehicleService:ApiKey"];

            _httpClient.BaseAddress = new Uri(baseUrl);
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("X-API-Key", apiKey);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync($"/api/vehicles/{Uri.EscapeDataString(registrationNumber)}", cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogWarning("Vehicle not found for registration number: {RegistrationNumber}", registrationNumber);
                return null;
            }

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync(cancellationToken);
            var vehicle = JsonSerializer.Deserialize<VehicleInfoDto>(content, _jsonOptions);

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