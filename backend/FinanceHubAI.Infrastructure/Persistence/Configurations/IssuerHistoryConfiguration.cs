using FinanceHubAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHubAI.Infrastructure.Persistence.Configurations;

public class IssuerHistoryConfiguration : IEntityTypeConfiguration<IssuerHistory>
{
    public void Configure(EntityTypeBuilder<IssuerHistory> builder)
    {
        builder.ToTable("IssuerHistories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IssuerId)
            .IsRequired();

        builder.Property(x => x.Action)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.LegalName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.City)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Street)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.Website)
            .HasMaxLength(250);

        builder.Property(x => x.Email)
            .HasMaxLength(250);

        builder.Property(x => x.PhoneNumber)
            .HasMaxLength(50);

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.IsMarketable)
            .IsRequired();

        builder.Property(x => x.IsPublicCompany)
            .IsRequired();

        builder.Property(x => x.MarketCap)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.StockSymbol)
            .HasMaxLength(50);

        builder.Property(x => x.ChangedByUserId)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.ChangedByUserName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.ChangedAt)
            .IsRequired();

        builder.Property(x => x.Reason)
            .HasMaxLength(500);

        builder.HasIndex(x => x.IssuerId);
        builder.HasIndex(x => x.ChangedAt);

        builder.HasOne<Issuer>()
            .WithMany()
            .HasForeignKey(x => x.IssuerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}