using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ThreadPilot.Application.Common.Interfaces;
using ThreadPilot.Domain.Common;
using ThreadPilot.Domain.Entities;
using ThreadPilot.Persistence.Seeds;

namespace ThreadPilot.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTime dateTime)
        : base(options)
    {
        _dateTime = dateTime;
    }

    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<InsuranceType> InsuranceTypes => Set<InsuranceType>();
    public DbSet<InsurancePolicy> InsurancePolicies => Set<InsurancePolicy>();
    public DbSet<Currency> Currencies => Set<Currency>();
    public DbSet<VehicleInsuranceType> VehicleInsuranceTypes => Set<VehicleInsuranceType>();
    public DbSet<VehicleInsuranceDetail> VehicleInsuranceDetails => Set<VehicleInsuranceDetail>();
    public DbSet<PetInsuranceType> PetInsuranceTypes => Set<PetInsuranceType>();
    public DbSet<PetInsuranceDetail> PetInsuranceDetails => Set<PetInsuranceDetail>();
    public DbSet<HealthInsuranceType> HealthInsuranceTypes => Set<HealthInsuranceType>();
    public DbSet<HealthInsuranceDetail> HealthInsuranceDetails => Set<HealthInsuranceDetail>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Apply seed data
        InsuranceTypesSeeder.SeedInsuranceTypes(modelBuilder);
        DummyDataSeeder.SeedDummyData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = _dateTime.UtcNow;
                    entry.Entity.UpdatedAt = _dateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = _dateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}