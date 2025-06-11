using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThreadPilot.Domain.Entities;

namespace ThreadPilot.Persistence.Configurations;

public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currencies");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("currency_id")
            .ValueGeneratedNever();

        builder.Property(c => c.Code)
            .HasColumnName("code")
            .HasMaxLength(3)
            .IsRequired();

        builder.Property(c => c.Symbol)
            .HasColumnName("symbol")
            .HasMaxLength(5)
            .IsRequired();

        builder.HasIndex(c => c.Code)
            .IsUnique();
    }
}