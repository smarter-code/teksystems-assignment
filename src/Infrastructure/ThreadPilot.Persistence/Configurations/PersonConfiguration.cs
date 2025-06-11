using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("persons");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("person_id");

        builder.Property(p => p.PersonalIdentificationNumber)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        // Unique index for fast lookups and uniqueness constraint
        builder.HasIndex(p => p.PersonalIdentificationNumber)
            .IsUnique()
            .HasDatabaseName("idx_persons_pin");

        // Navigation
        builder.HasMany(p => p.InsurancePolicies)
            .WithOne(ip => ip.Person)
            .HasForeignKey(ip => ip.PersonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}