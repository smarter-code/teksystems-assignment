using FluentValidation;

namespace ThreadPilot.Application.Features.Vehicles.Queries.GetVehicleByRegistrationNumber;

public class GetVehicleByRegistrationNumberQueryValidator : AbstractValidator<GetVehicleByRegistrationNumberQuery>
{
    public GetVehicleByRegistrationNumberQueryValidator()
    {
        RuleFor(x => x.RegistrationNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Registration number is required.")
            .Length(6).WithMessage("Registration number must be exactly 6 characters.")
            .Matches(@"^[A-Za-z]{3}\d{2}[A-Za-z\d]$").WithMessage("Registration number must be in format ABC123 or ABC12D.");
    }
}