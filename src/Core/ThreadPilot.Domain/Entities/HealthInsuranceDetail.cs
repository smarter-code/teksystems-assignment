using ThreadPilot.Domain.Common;

namespace ThreadPilot.Domain.Entities;

public class HealthInsuranceDetail : BaseEntity
{
    public long PolicyId { get; set; }
    public int HealthInsuranceTypeId { get; set; }
    public decimal MonthlyCost { get; set; }
    public int CurrencyId { get; set; }

    public virtual InsurancePolicy Policy { get; set; }
    public virtual HealthInsuranceType HealthInsuranceType { get; set; }
    public virtual Currency Currency { get; set; }
}