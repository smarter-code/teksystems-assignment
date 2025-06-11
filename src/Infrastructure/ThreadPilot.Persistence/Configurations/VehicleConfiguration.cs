using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
            .HasColumnName("vehicle_id");

        builder.Property(v => v.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(6);

        builder.Property(v => v.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(v => v.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        // Unique index for fast lookups and uniqueness constraint
        builder.HasIndex(v => v.RegistrationNumber)
            .IsUnique();

        // Navigation
        builder.HasMany(v => v.VehicleInsuranceDetails)
            .WithOne(vid => vid.Vehicle)
            .HasForeignKey(vid => vid.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}