using Microsoft.Extensions.DependencyInjection;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Infrastructure.Services;
using Microsoft.Extensions.Configuration;

namespace ThreadPilot.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IPersonalNumberNormalizer, PersonalNumberNormalizer>();

        // Register HTTP client for VehicleService
        services.AddHttpClient<IVehicleServiceClient, VehicleServiceClient>((provider, client) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            client.BaseAddress = new Uri(configuration["VehicleService:BaseUrl"]);
            client.DefaultRequestHeaders.Add("X-API-Key", configuration["VehicleService:ApiKey"]);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(30);
        });

        services.AddMemoryCache();

        return services;
    }
}