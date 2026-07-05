using FinanceHubAI.Domain.Enums;

namespace FinanceHubAI.Api.Contracts.Issuers;

public sealed record CreateIssuerRequest(
    string Name,
    string LegalName,
    string RegistrationNumber,
    IssuerType Type,
    string Country,
    string City,
    string Street,
    string? Website,
    string? Email,
    string? PhoneNumber,
    bool IsMarketable,
    bool IsPublicCompany,
    decimal? MarketCap,
    string? StockSymbol
);