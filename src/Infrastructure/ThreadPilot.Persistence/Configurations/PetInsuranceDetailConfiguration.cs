using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class PetInsuranceDetailConfiguration : IEntityTypeConfiguration<PetInsuranceDetail>
{
    public void Configure(EntityTypeBuilder<PetInsuranceDetail> builder)
    {
        builder.ToTable("pet_insurance_details");

        builder.HasKey(pid => pid.Id);

        builder.Property(pid => pid.Id)
            .HasColumnName("pet_insurance_id");

        builder.Property(pid => pid.PolicyId)
            .HasColumnName("policy_id")
            .IsRequired();

        builder.Property(pid => pid.PetInsuranceTypeId)
            .HasColumnName("pet_insurance_type_id")
            .IsRequired();

        builder.Property(pid => pid.MonthlyCost)
            .HasColumnName("monthly_cost")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(pid => pid.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.Property(pid => pid.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(pid => pid.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(pid => pid.PolicyId)
            .HasDatabaseName("idx_pet_details_policy");

        builder.HasOne(pid => pid.Policy)
            .WithOne(ip => ip.PetInsuranceDetail)
            .HasForeignKey<PetInsuranceDetail>(pid => pid.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pid => pid.PetInsuranceType)
            .WithMany(pit => pit.PetInsuranceDetails)
            .HasForeignKey(pid => pid.PetInsuranceTypeId);

        builder.HasOne(pid => pid.Currency)
            .WithMany()
            .HasForeignKey(pid => pid.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}