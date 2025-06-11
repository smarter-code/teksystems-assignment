
namespace ThreadPilot.Domain.Entities;

public class VehicleInsuranceType
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public decimal BaseMonthlyCost { get; set; }
    public int CurrencyId { get; set; }

    public virtual Currency Currency { get; set; }
    public virtual ICollection<VehicleInsuranceDetail> VehicleInsuranceDetails { get; set; }

    public VehicleInsuranceType()
    {
        VehicleInsuranceDetails = new HashSet<VehicleInsuranceDetail>();
    }
}