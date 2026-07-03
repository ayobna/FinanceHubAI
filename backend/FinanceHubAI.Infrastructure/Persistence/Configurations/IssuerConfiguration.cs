using FinanceHubAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceHubAI.Infrastructure.Persistence.Configurations;

public class IssuerConfiguration : IEntityTypeConfiguration<Issuer>
{
    public void Configure(EntityTypeBuilder<Issuer> builder)
    {
        builder.ToTable("Issuers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.LegalName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(x => x.RegistrationNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.RegistrationNumber)
            .IsUnique();

        builder.Property(x => x.Type)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(x => x.Country)
                .HasColumnName("Country")
                .IsRequired()
                .HasMaxLength(100);

            address.Property(x => x.City)
                .HasColumnName("City")
                .IsRequired()
                .HasMaxLength(100);

            address.Property(x => x.Street)
                .HasColumnName("Street")
                .IsRequired()
                .HasMaxLength(250);
        });

        builder.OwnsOne(x => x.ContactInformation, contact =>
        {
            contact.Property(x => x.Website)
                .HasColumnName("Website")
                .HasMaxLength(250);

            contact.Property(x => x.Email)
                .HasColumnName("Email")
                .HasMaxLength(250);

            contact.Property(x => x.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(50);
        });

        builder.OwnsOne(x => x.MarketInformation, market =>
        {
            market.Property(x => x.IsMarketable)
                .HasColumnName("IsMarketable")
                .IsRequired();

            market.Property(x => x.IsPublicCompany)
                .HasColumnName("IsPublicCompany")
                .IsRequired();

            market.Property(x => x.MarketCap)
                .HasColumnName("MarketCap")
                .HasColumnType("decimal(18,2)");

            market.Property(x => x.StockSymbol)
                .HasColumnName("StockSymbol")
                .HasMaxLength(50);
        });
    }
}