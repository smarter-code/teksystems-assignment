using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class HealthInsuranceDetailConfiguration : IEntityTypeConfiguration<HealthInsuranceDetail>
{
    public void Configure(EntityTypeBuilder<HealthInsuranceDetail> builder)
    {
        builder.ToTable("health_insurance_details");

        builder.HasKey(hid => hid.Id);

        builder.Property(hid => hid.Id)
            .HasColumnName("health_insurance_id");

        builder.Property(hid => hid.PolicyId)
            .HasColumnName("policy_id")
            .IsRequired();

        builder.Property(hid => hid.HealthInsuranceTypeId)
            .HasColumnName("health_insurance_type_id")
            .IsRequired();

        builder.Property(hid => hid.MonthlyCost)
            .HasColumnName("monthly_cost")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(hid => hid.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.Property(hid => hid.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(hid => hid.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(hid => hid.PolicyId)
            .HasDatabaseName("idx_health_details_policy");

        builder.HasOne(hid => hid.Policy)
            .WithOne(ip => ip.HealthInsuranceDetail)
            .HasForeignKey<HealthInsuranceDetail>(hid => hid.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(hid => hid.HealthInsuranceType)
            .WithMany(hit => hit.HealthInsuranceDetails)
            .HasForeignKey(hid => hid.HealthInsuranceTypeId);

        builder.HasOne(hid => hid.Currency)
            .WithMany()
            .HasForeignKey(hid => hid.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}