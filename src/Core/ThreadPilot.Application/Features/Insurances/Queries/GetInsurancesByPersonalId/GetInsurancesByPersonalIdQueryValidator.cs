using FluentValidation;
using ThreadPilot.Application.Common.Interfaces;

namespace ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

public class GetInsurancesByPersonalIdQueryValidator : AbstractValidator<GetInsurancesByPersonalIdQuery>
{
    private readonly IPersonalNumberNormalizer _normalizer;

    public GetInsurancesByPersonalIdQueryValidator(IPersonalNumberNormalizer normalizer)
    {
        _normalizer = normalizer;

        RuleFor(x => x.PersonalIdentificationNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Personal identification number is required.")
            .Length(10).WithMessage("Personal identification number must be exactly 10 digits.")
            .Matches(@"^\d{10}$").WithMessage("Personal identification number must contain only digits.");
    }
}