using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ThreadPilot.Application.Common.Interfaces;

namespace ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;

public class GetVehicleByRegistrationNumberQueryHandler : IRequestHandler<GetVehicleByRegistrationNumberQuery, Result<VehicleDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetVehicleByRegistrationNumberQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<VehicleDto>> Handle(GetVehicleByRegistrationNumberQuery request, CancellationToken cancellationToken)
    {
        // Normalize registration number to uppercase
        var normalizedRegistrationNumber = request.RegistrationNumber?.ToUpperInvariant();

        var vehicle = await _context.Vehicles
            .AsNoTracking()
            .Where(v => v.RegistrationNumber == normalizedRegistrationNumber)
            .FirstOrDefaultAsync(cancellationToken);

        if (vehicle == null)
        {
            return Result.Fail<VehicleDto>($"Vehicle with registration number '{request.RegistrationNumber}' not found.");
        }

        var vehicleDto = _mapper.Map<VehicleDto>(vehicle);
        return Result.Ok(vehicleDto);
    }
}