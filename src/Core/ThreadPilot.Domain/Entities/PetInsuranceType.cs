
namespace ThreadPilot.Domain.Entities;

public class PetInsuranceType
{
    public int Id { get; set; }
    public string TypeName { get; set; }
    public decimal BaseMonthlyCost { get; set; }
    public int CurrencyId { get; set; }

    public virtual Currency Currency { get; set; }
    public virtual ICollection<PetInsuranceDetail> PetInsuranceDetails { get; set; }

    public PetInsuranceType()
    {
        PetInsuranceDetails = new HashSet<PetInsuranceDetail>();
    }
}