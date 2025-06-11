
namespace ThreadPilot.Domain.Entities;

public class HealthInsuranceType
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public decimal BaseMonthlyCost { get; set; }
    public int CurrencyId { get; set; }

    public virtual Currency Currency { get; set; }
    public virtual ICollection<HealthInsuranceDetail> HealthInsuranceDetails { get; set; }

    public HealthInsuranceType()
    {
        HealthInsuranceDetails = new HashSet<HealthInsuranceDetail>();
    }
}