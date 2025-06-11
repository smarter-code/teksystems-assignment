using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Person> Persons { get; }
    DbSet<Vehicle> Vehicles { get; }
    DbSet<InsuranceType> InsuranceTypes { get; }
    DbSet<InsurancePolicy> InsurancePolicies { get; }
    DbSet<Currency> Currencies { get; }
    DbSet<VehicleInsuranceType> VehicleInsuranceTypes { get; }
    DbSet<VehicleInsuranceDetail> VehicleInsuranceDetails { get; }
    DbSet<PetInsuranceType> PetInsuranceTypes { get; }
    DbSet<PetInsuranceDetail> PetInsuranceDetails { get; }
    DbSet<HealthInsuranceType> HealthInsuranceTypes { get; }
    DbSet<HealthInsuranceDetail> HealthInsuranceDetails { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}