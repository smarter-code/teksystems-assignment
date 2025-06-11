using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class PetInsuranceTypeConfiguration : IEntityTypeConfiguration<PetInsuranceType>
{
    public void Configure(EntityTypeBuilder<PetInsuranceType> builder)
    {
        builder.ToTable("pet_insurance_types");

        builder.HasKey(pit => pit.Id);

        builder.Property(pit => pit.Id)
            .HasColumnName("pet_insurance_type_id");

        builder.Property(pit => pit.TypeName)
            .HasColumnName("type_name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(pit => pit.BaseMonthlyCost)
            .HasColumnName("base_monthly_cost")
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(pit => pit.CurrencyId)
            .HasColumnName("currency_id")
            .IsRequired();

        builder.HasOne(pit => pit.Currency)
            .WithMany()
            .HasForeignKey(pit => pit.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(pit => pit.PetInsuranceDetails)
            .WithOne(pid => pid.PetInsuranceType)
            .HasForeignKey(pid => pid.PetInsuranceTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}