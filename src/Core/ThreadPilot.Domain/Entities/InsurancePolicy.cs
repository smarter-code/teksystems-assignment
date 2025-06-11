using ThreadPilot.Domain.Common;

namespace ThreadPilot.Domain.Entities;

public class InsurancePolicy : BaseEntity
{
    public string PolicyNumber { get; set; }
    public long PersonId { get; set; }
    public int InsuranceTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Navigation properties
    public virtual Person Person { get; set; }
    public virtual InsuranceType InsuranceType { get; set; }
    public virtual VehicleInsuranceDetail VehicleInsuranceDetail { get; set; }
    public virtual PetInsuranceDetail PetInsuranceDetail { get; set; }
    public virtual HealthInsuranceDetail HealthInsuranceDetail { get; set; }
}