using ThreadPilot.Application.Common.Mappings;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;

public class VehicleDto : IMapFrom<Vehicle>
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public string Model { get; set; }
}