using Microsoft.EntityFrameworkCore;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Seeds;

public static class InsuranceTypesSeeder
{
    public static void SeedInsuranceTypes(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>().HasData(
            new Currency { Id = 1, Code = "USD", Symbol = "$" },
            new Currency { Id = 2, Code = "EUR", Symbol = "€" },
            new Currency { Id = 3, Code = "SEK", Symbol = "kr" }
        );

        modelBuilder.Entity<InsuranceType>().HasData(
            new InsuranceType { Id = 1, TypeName = "Pet" },
            new InsuranceType { Id = 2, TypeName = "Health" },
            new InsuranceType { Id = 3, TypeName = "Vehicle" }
        );

        modelBuilder.Entity<VehicleInsuranceType>().HasData(
            new { Id = 1, TypeName = "Vehicle Insurance", BaseMonthlyCost = 30m, CurrencyId = 1 }
        );

        modelBuilder.Entity<PetInsuranceType>().HasData(
            new { Id = 1, TypeName = "Pet Insurance", BaseMonthlyCost = 10m, CurrencyId = 1 }
        );

        modelBuilder.Entity<HealthInsuranceType>().HasData(
            new { Id = 1, TypeName = "Health Insurance", BaseMonthlyCost = 20m, CurrencyId = 1 }
        );
    }
}