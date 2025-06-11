using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class InsuranceTypeConfiguration : IEntityTypeConfiguration<InsuranceType>
{
    public void Configure(EntityTypeBuilder<InsuranceType> builder)
    {
        builder.ToTable("insurance_types");

        builder.HasKey(it => it.Id);

        builder.Property(it => it.Id)
            .HasColumnName("insurance_type_id")
            .ValueGeneratedNever(); // We'll manually set these IDs

        builder.Property(it => it.TypeName)
            .HasColumnName("type_name")
            .HasMaxLength(50)
            .IsRequired();

        // Navigation
        builder.HasMany(it => it.InsurancePolicies)
            .WithOne(ip => ip.InsuranceType)
            .HasForeignKey(ip => ip.InsuranceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}