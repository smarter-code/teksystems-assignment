using ThreadPilot.Domain.Common;

namespace ThreadPilot.Domain.Entities;

public class Person : BaseEntity
{
    public string PersonalIdentificationNumber { get; set; }

    // Navigation properties
    public virtual ICollection<InsurancePolicy> InsurancePolicies { get; set; }

    public Person()
    {
        InsurancePolicies = new HashSet<InsurancePolicy>();
    }
}