using FinanceHubAI.Domain.Common;
using FinanceHubAI.Domain.Enums;

namespace FinanceHubAI.Domain.Entities;

public class IssuerHistory : BaseEntity
{
    public Guid IssuerId { get; private set; }

    public IssuerHistoryAction Action { get; private set; }

    public string Name { get; private set; } = null!;
    public string LegalName { get; private set; } = null!;
    public string RegistrationNumber { get; private set; } = null!;

    public string Country { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;

    public string? Website { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }

    public IssuerType Type { get; private set; }
    public IssuerStatus Status { get; private set; }

    public bool IsMarketable { get; private set; }
    public bool IsPublicCompany { get; private set; }

    public decimal? MarketCap { get; private set; }
    public string? StockSymbol { get; private set; }

    public string ChangedByUserId { get; private set; } = null!;
    public string ChangedByUserName { get; private set; } = null!;
    public DateTime ChangedAt { get; private set; }

    public string? Reason { get; private set; }

    private IssuerHistory()
    {
    }

    private IssuerHistory(
        Issuer issuer,
        IssuerHistoryAction action,
        string changedByUserId,
        string changedByUserName,
        string? reason)
    {
        IssuerId = issuer.Id;
        Action = action;

        Name = issuer.Name;
        LegalName = issuer.LegalName;
        RegistrationNumber = issuer.RegistrationNumber;

        Country = issuer.Address.Country;
        City = issuer.Address.City;
        Street = issuer.Address.Street;

        Website = issuer.ContactInformation.Website;
        Email = issuer.ContactInformation.Email;
        PhoneNumber = issuer.ContactInformation.PhoneNumber;

        Type = issuer.Type;
        Status = issuer.Status;

        IsMarketable = issuer.MarketInformation.IsMarketable;
        IsPublicCompany = issuer.MarketInformation.IsPublicCompany;
        MarketCap = issuer.MarketInformation.MarketCap;
        StockSymbol = issuer.MarketInformation.StockSymbol;

        ChangedByUserId = changedByUserId;
        ChangedByUserName = changedByUserName;
        ChangedAt = DateTime.UtcNow;
        Reason = reason;
    }

    public static IssuerHistory CreateSnapshot(
        Issuer issuer,
        IssuerHistoryAction action,
        string changedByUserId,
        string changedByUserName,
        string? reason = null)
    {
        if (issuer is null)
            throw new ArgumentNullException(nameof(issuer));

        if (string.IsNullOrWhiteSpace(changedByUserId))
            throw new ArgumentException("Changed by user id is required.");

        if (string.IsNullOrWhiteSpace(changedByUserName))
            throw new ArgumentException("Changed by user name is required.");

        return new IssuerHistory(
            issuer,
            action,
            changedByUserId,
            changedByUserName,
            reason);
    }
}