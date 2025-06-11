using ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

namespace ThreadPilot.Application.Common.Interfaces;

public interface IVehicleServiceClient
{
    Task<VehicleInfoDto> GetVehicleAsync(string registrationNumber, CancellationToken cancellationToken);
}