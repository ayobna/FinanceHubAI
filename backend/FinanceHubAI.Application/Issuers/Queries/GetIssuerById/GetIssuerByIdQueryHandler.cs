using FinanceHubAI.Application.Common.Interfaces;
using FinanceHubAI.Application.Common.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHubAI.Application.Issuers.Queries.GetIssuerById;

public sealed class GetIssuerByIdQueryHandler: IRequestHandler<GetIssuerByIdQuery, Result<IssuerDto>>
{
    private readonly IFinanceHubDbContext _context;

    public GetIssuerByIdQueryHandler(IFinanceHubDbContext context)
    {
        _context = context;
    }

    public async Task<Result<IssuerDto>> Handle(
        GetIssuerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var issuer = await _context.Issuers
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.Id,
                cancellationToken);

        if (issuer is null)
        {
            return Result<IssuerDto>.Failure(
                new Error(
                    "Issuer.NotFound",
                    "Issuer was not found."));
        }

        var dto = new IssuerDto(
            issuer.Id,
            issuer.Name,
            issuer.LegalName,
            issuer.RegistrationNumber,
            issuer.Type,
            issuer.Status,
            issuer.Address.Country,
            issuer.Address.City,
            issuer.Address.Street,
            issuer.ContactInformation.Website,
            issuer.ContactInformation.Email,
            issuer.ContactInformation.PhoneNumber,
            issuer.MarketInformation.IsMarketable,
            issuer.MarketInformation.IsPublicCompany,
            issuer.MarketInformation.MarketCap,
            issuer.MarketInformation.StockSymbol);

        return Result<IssuerDto>.Success(dto);
    }
}