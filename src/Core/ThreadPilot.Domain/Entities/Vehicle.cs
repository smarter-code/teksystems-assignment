using ThreadPilot.Domain.Common;

namespace ThreadPilot.Domain.Entities;

public class Vehicle : BaseEntity
{
    public string RegistrationNumber { get; set; }
    public string Color { get; set; }
    public string Model { get; set; }
    public virtual ICollection<VehicleInsuranceDetail> VehicleInsuranceDetails { get; set; }

    public Vehicle()
    {
        VehicleInsuranceDetails = new HashSet<VehicleInsuranceDetail>();
    }
}