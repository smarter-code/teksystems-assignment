using ThreadPilot.Domain.Common;

namespace ThreadPilot.Domain.Entities;

public class VehicleInsuranceDetail : BaseEntity
{
    public long PolicyId { get; set; }
    public long VehicleId { get; set; }
    public int VehicleInsuranceTypeId { get; set; }
    public decimal MonthlyCost { get; set; }
    public int CurrencyId { get; set; }

    public virtual InsurancePolicy Policy { get; set; }
    public virtual Vehicle Vehicle { get; set; }
    public virtual VehicleInsuranceType VehicleInsuranceType { get; set; }
    public virtual Currency Currency { get; set; }
}