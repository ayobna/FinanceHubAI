using FinanceHubAI.Domain.Enums;
using MediatR;

namespace FinanceHubAI.Application.Issuers.Commands.CreateIssuer;

public sealed record CreateIssuerCommand(
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
) : IRequest<Guid>;