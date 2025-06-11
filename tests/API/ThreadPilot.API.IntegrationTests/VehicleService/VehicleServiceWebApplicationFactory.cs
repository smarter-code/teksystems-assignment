using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ThreadPilot.Domain.Entities;
using ThreadPilot.Persistence.Context;

namespace ThreadPilot.API.IntegrationTests.VehicleService;

public class VehicleServiceWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's ApplicationDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Add ApplicationDbContext using an in-memory database for testing.
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                // Use a fixed name so the test and app share the same in-memory database
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Build the service provider.
            var sp = services.BuildServiceProvider();

            // Create a scope to obtain a reference to the database
            using (var scope = sp.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();

                // Don't call Migrate() on in-memory database
                // Just ensure the database is created
                db.Database.EnsureCreated();
            }
        });

        builder.UseEnvironment("Testing");
    }
}