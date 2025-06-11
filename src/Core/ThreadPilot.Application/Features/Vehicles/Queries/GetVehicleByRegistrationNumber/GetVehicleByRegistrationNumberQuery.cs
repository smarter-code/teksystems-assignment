using FluentResults;
using MediatR;

namespace ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;

public record GetVehicleByRegistrationNumberQuery(string RegistrationNumber) : IRequest<Result<VehicleDto>>;