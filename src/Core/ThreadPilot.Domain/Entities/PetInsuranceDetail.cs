using ThreadPilot.Domain.Common;

namespace ThreadPilot.Domain.Entities;

public class PetInsuranceDetail : BaseEntity
{
    public long PolicyId { get; set; }
    public int PetInsuranceTypeId { get; set; }
    public decimal MonthlyCost { get; set; }
    public int CurrencyId { get; set; }

    public virtual InsurancePolicy Policy { get; set; }
    public virtual PetInsuranceType PetInsuranceType { get; set; }
    public virtual Currency Currency { get; set; }
}