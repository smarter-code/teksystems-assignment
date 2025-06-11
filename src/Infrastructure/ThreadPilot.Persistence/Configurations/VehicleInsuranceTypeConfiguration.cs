using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class VehicleInsuranceTypeConfiguration : IEntityTypeConfiguration<VehicleInsuranceType>
{
    public void Configure(EntityTypeBuilder<VehicleInsuranceType> builder)
    {
        builder.ToTable("vehicle_insurance_types");

        builder.HasKey(vit => vit.Id);

        builder.Property(vit => vit.Id)
            .HasColumnName("vehicle_insurance_type_id");

        builder.Property(vit => vit.TypeName)
            .HasColumnName("type_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(vit => vit.BaseMonthlyCost)
            .HasColumnName("base_monthly_cost")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(vit => vit.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.HasOne(vit => vit.Currency)
            .WithMany()
            .HasForeignKey(vit => vit.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(vit => vit.VehicleInsuranceDetails)
            .WithOne(vid => vid.VehicleInsuranceType)
            .HasForeignKey(vid => vid.VehicleInsuranceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}