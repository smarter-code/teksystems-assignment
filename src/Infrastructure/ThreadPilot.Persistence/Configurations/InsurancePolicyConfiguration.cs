using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class InsurancePolicyConfiguration : IEntityTypeConfiguration<InsurancePolicy>
{
    public void Configure(EntityTypeBuilder<InsurancePolicy> builder)
    {
        builder.ToTable("insurance_policies");

        builder.HasKey(ip => ip.Id);

        builder.Property(ip => ip.Id)
            .HasColumnName("policy_id");

        builder.Property(ip => ip.PolicyNumber)
            .HasColumnName("policy_number")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(ip => ip.PersonId)
            .HasColumnName("person_id")
            .IsRequired();

        builder.Property(ip => ip.InsuranceTypeId)
            .HasColumnName("insurance_type_id")
            .IsRequired();

        builder.Property(ip => ip.StartDate)
            .HasColumnName("start_date")
            .IsRequired();

        builder.Property(ip => ip.EndDate)
            .HasColumnName("end_date");

        builder.Property(ip => ip.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(ip => ip.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(ip => ip.PolicyNumber)
            .IsUnique();

        builder.HasIndex(ip => new { ip.PersonId, ip.InsuranceTypeId })
            .HasDatabaseName("idx_policies_person_type");

        builder.HasOne(ip => ip.Person)
            .WithMany(p => p.InsurancePolicies)
            .HasForeignKey(ip => ip.PersonId);

        builder.HasOne(ip => ip.InsuranceType)
            .WithMany(it => it.InsurancePolicies)
            .HasForeignKey(ip => ip.InsuranceTypeId);

        builder.HasOne(ip => ip.VehicleInsuranceDetail)
            .WithOne(vid => vid.Policy)
            .HasForeignKey<VehicleInsuranceDetail>(vid => vid.PolicyId);

        builder.HasOne(ip => ip.PetInsuranceDetail)
            .WithOne(pid => pid.Policy)
            .HasForeignKey<PetInsuranceDetail>(pid => pid.PolicyId);

        builder.HasOne(ip => ip.HealthInsuranceDetail)
            .WithOne(hid => hid.Policy)
            .HasForeignKey<HealthInsuranceDetail>(hid => hid.PolicyId);
    }
}