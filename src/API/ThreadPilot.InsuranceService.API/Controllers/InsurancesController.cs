using MediatR;
using Microsoft.AspNetCore.Mvc;
using ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

namespace ThreadPilot.InsuranceService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsurancesController : ControllerBase
{
    private readonly IMediator _mediator;

    public InsurancesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("person/{personalIdentificationNumber}")]
    [ProducesResponseType(typeof(PersonInsurancesDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetByPersonalIdentificationNumber(string personalIdentificationNumber)
    {
        var query = new GetInsurancesByPersonalIdQuery(personalIdentificationNumber);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(new { detail = result.Errors.FirstOrDefault()?.Message });
    }
}