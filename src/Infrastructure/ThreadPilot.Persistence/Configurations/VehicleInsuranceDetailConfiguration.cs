using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class VehicleInsuranceDetailConfiguration : IEntityTypeConfiguration<VehicleInsuranceDetail>
{
    public void Configure(EntityTypeBuilder<VehicleInsuranceDetail> builder)
    {
        builder.ToTable("vehicle_insurance_details");

        builder.HasKey(vid => vid.Id);

        builder.Property(vid => vid.Id)
            .HasColumnName("vehicle_insurance_id");

        builder.Property(vid => vid.PolicyId)
            .HasColumnName("policy_id")
            .IsRequired();

        builder.Property(vid => vid.VehicleId)
            .HasColumnName("vehicle_id")
            .IsRequired();

        builder.Property(vid => vid.VehicleInsuranceTypeId)
            .HasColumnName("vehicle_insurance_type_id")
            .IsRequired();

        builder.Property(vid => vid.MonthlyCost)
            .HasColumnName("monthly_cost")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(vid => vid.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.Property(vid => vid.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(vid => vid.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(vid => vid.PolicyId)
            .HasDatabaseName("idx_vehicle_details_policy");

        builder.HasOne(vid => vid.Policy)
            .WithOne(ip => ip.VehicleInsuranceDetail)
            .HasForeignKey<VehicleInsuranceDetail>(vid => vid.PolicyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(vid => vid.Vehicle)
            .WithMany(v => v.VehicleInsuranceDetails)
            .HasForeignKey(vid => vid.VehicleId);

        builder.HasOne(vid => vid.VehicleInsuranceType)
            .WithMany(vit => vit.VehicleInsuranceDetails)
            .HasForeignKey(vid => vid.VehicleInsuranceTypeId);

        builder.HasOne(vid => vid.Currency)
            .WithMany()
            .HasForeignKey(vid => vid.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}