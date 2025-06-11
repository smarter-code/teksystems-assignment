namespace ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

public class PersonInsurancesDto
{
    public string PersonalIdentificationNumber { get; set; }
    public List<VehicleInsuranceDto> VehicleInsurances { get; set; } = new();
    public List<HealthInsuranceDto> HealthInsurances { get; set; } = new();
    public List<PetInsuranceDto> PetInsurances { get; set; } = new();
}