using FluentResults;
using MediatR;

namespace ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

public record GetInsurancesByPersonalIdQuery(string PersonalIdentificationNumber) : IRequest<Result<PersonInsurancesDto>>;