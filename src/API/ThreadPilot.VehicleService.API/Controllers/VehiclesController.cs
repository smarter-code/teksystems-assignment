using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;

namespace ThreadPilot.VehicleService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehiclesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<GetVehicleByRegistrationNumberQuery> _validator;

    public VehiclesController(IMediator mediator, IValidator<GetVehicleByRegistrationNumberQuery> validator)
    {
        _mediator = mediator;
        _validator = validator;
    }

    [HttpGet("{registrationNumber}")]
    [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetByRegistrationNumber(string registrationNumber)
    {
        var query = new GetVehicleByRegistrationNumberQuery(registrationNumber);

        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(new { detail = result.Errors.FirstOrDefault()?.Message });
    }
}