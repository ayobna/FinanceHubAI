using FinanceHubAI.Domain.Enums;

namespace FinanceHubAI.Application.Issuers.Queries.GetIssuerById;

public sealed record IssuerDto(
    Guid Id,
    string Name,
    string LegalName,
    string RegistrationNumber,
    IssuerType Type,
    IssuerStatus Status,
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