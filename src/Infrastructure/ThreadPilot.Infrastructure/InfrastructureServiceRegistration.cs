using Microsoft.Extensions.DependencyInjection;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Infrastructure.Services;

namespace ThreadPilot.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IPersonalNumberNormalizer, PersonalNumberNormalizer>();

        // Register HTTP client for VehicleService
        services.AddHttpClient<IVehicleServiceClient, VehicleServiceClient>()
            .ConfigureHttpClient((client) =>
            {
                client.Timeout = TimeSpan.FromSeconds(30);
            });

        return services;
    }
}