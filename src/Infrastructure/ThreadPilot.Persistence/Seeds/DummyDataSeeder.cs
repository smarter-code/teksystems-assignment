using Microsoft.EntityFrameworkCore;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Seeds;

public static class DummyDataSeeder
{
    private static readonly DateTime _seedDate = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static void SeedDummyData(ModelBuilder modelBuilder)
    {
        var persons = new[]
        {
            new { Id = 1L, PersonalIdentificationNumber = "8905152384", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 2L, PersonalIdentificationNumber = "9211302391", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 3L, PersonalIdentificationNumber = "8501011234", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 4L, PersonalIdentificationNumber = "9512245678", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 5L, PersonalIdentificationNumber = "0107089012", CreatedAt = _seedDate, UpdatedAt = _seedDate }
        };
        modelBuilder.Entity<Person>().HasData(persons);

        var vehicles = new[]
        {
            new { Id = 1L, RegistrationNumber = "ABC123", Color = "Red", Model = "Tesla Model 3", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 2L, RegistrationNumber = "XYZ789", Color = "Blue", Model = "BMW X5", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 3L, RegistrationNumber = "KLM456", Color = "White", Model = "Mercedes C-Class", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 4L, RegistrationNumber = "DEF12A", Color = "Black", Model = "Audi A4", CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 5L, RegistrationNumber = "GHI34B", Color = "Silver", Model = "Volvo XC90", CreatedAt = _seedDate, UpdatedAt = _seedDate }
        };
        modelBuilder.Entity<Vehicle>().HasData(vehicles);

        var policies = new[]
        {
            new { Id = 1L, PolicyNumber = "POL-001-PET", PersonId = 1L, InsuranceTypeId = 1, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 2L, PolicyNumber = "POL-001-HEALTH", PersonId = 1L, InsuranceTypeId = 2, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 3L, PolicyNumber = "POL-001-VEH", PersonId = 1L, InsuranceTypeId = 3, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 4L, PolicyNumber = "POL-002-HEALTH", PersonId = 2L, InsuranceTypeId = 2, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 5L, PolicyNumber = "POL-002-VEH", PersonId = 2L, InsuranceTypeId = 3, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 6L, PolicyNumber = "POL-003-PET", PersonId = 3L, InsuranceTypeId = 1, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 7L, PolicyNumber = "POL-004-VEH", PersonId = 4L, InsuranceTypeId = 3, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 8L, PolicyNumber = "POL-004-HEALTH", PersonId = 4L, InsuranceTypeId = 2, StartDate = _seedDate, EndDate = (DateTime?)null, CreatedAt = _seedDate, UpdatedAt = _seedDate }
        };
        modelBuilder.Entity<InsurancePolicy>().HasData(policies);

        modelBuilder.Entity<PetInsuranceDetail>().HasData(
            new { Id = 1L, PolicyId = 1L, PetInsuranceTypeId = 1, MonthlyCost = 10m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 2L, PolicyId = 6L, PetInsuranceTypeId = 1, MonthlyCost = 10m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate }
        );

        modelBuilder.Entity<HealthInsuranceDetail>().HasData(
            new { Id = 1L, PolicyId = 2L, HealthInsuranceTypeId = 1, MonthlyCost = 20m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 2L, PolicyId = 4L, HealthInsuranceTypeId = 1, MonthlyCost = 20m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 3L, PolicyId = 8L, HealthInsuranceTypeId = 1, MonthlyCost = 20m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate }
        );

        modelBuilder.Entity<VehicleInsuranceDetail>().HasData(
            new { Id = 1L, PolicyId = 3L, VehicleId = 1L, VehicleInsuranceTypeId = 1, MonthlyCost = 30m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 2L, PolicyId = 5L, VehicleId = 2L, VehicleInsuranceTypeId = 1, MonthlyCost = 30m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate },
            new { Id = 3L, PolicyId = 7L, VehicleId = 4L, VehicleInsuranceTypeId = 1, MonthlyCost = 30m, CurrencyId = 1, CreatedAt = _seedDate, UpdatedAt = _seedDate }
        );
    }
}