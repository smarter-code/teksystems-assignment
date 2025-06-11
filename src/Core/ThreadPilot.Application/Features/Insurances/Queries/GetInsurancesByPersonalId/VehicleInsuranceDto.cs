namespace ThreadPilot.Application.Features.Insurances.Queries.GetInsurancesByPersonalId;

public class VehicleInsuranceDto
{
    public string PolicyNumber { get; set; }
    public string InsuranceTypeName { get; set; }
    public decimal MonthlyCost { get; set; }
    public string Currency { get; set; }
    public VehicleInfoDto VehicleInfo { get; set; }
}