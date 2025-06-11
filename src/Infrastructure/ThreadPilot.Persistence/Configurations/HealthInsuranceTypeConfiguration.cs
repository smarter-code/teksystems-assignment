using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class HealthInsuranceTypeConfiguration : IEntityTypeConfiguration<HealthInsuranceType>
{
    public void Configure(EntityTypeBuilder<HealthInsuranceType> builder)
    {
        builder.ToTable("health_insurance_types");

        builder.HasKey(hit => hit.Id);

        builder.Property(hit => hit.Id)
            .HasColumnName("health_insurance_type_id");

        builder.Property(hit => hit.TypeName)
            .HasColumnName("type_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(hit => hit.BaseMonthlyCost)
            .HasColumnName("base_monthly_cost")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(hit => hit.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.HasOne(hit => hit.Currency)
            .WithMany()
            .HasForeignKey(hit => hit.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(hit => hit.HealthInsuranceDetails)
            .WithOne(hid => hid.HealthInsuranceType)
            .HasForeignKey(hid => hid.HealthInsuranceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}