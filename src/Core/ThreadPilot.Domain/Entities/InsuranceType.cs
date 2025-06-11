namespace ThreadPilot.Domain.Entities;

public class InsuranceType
{
    public int Id { get; set; }
    public string TypeName { get; set; }

    // Navigation properties
    public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; }

    public InsuranceType()
    {
        InsurancePolicies = new HashSet<InsurancePolicy>();
    }
}