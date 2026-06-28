using FinanceHubAI.Application.Common.Interfaces;
using FinanceHubAI.Domain.Entities;
using FinanceHubAI.Domain.Enums;
using FinanceHubAI.Domain.ValueObjects;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceHubAI.Application.Issuers.Commands.CreateIssuer;

public sealed class CreateIssuerCommandHandler
    : IRequestHandler<CreateIssuerCommand, Guid>
{
    private readonly IFinanceHubDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateIssuerCommandHandler(
        IFinanceHubDbContext context,
        ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(
        CreateIssuerCommand request,
        CancellationToken cancellationToken)
    {
        var registrationNumberExists = await _context.Issuers
            .AnyAsync(x => x.RegistrationNumber == request.RegistrationNumber, cancellationToken);

        if (registrationNumberExists)
            throw new InvalidOperationException("Issuer registration number already exists.");

        var address = Address.Create(
            request.Country,
            request.City,
            request.Street);

        var contactInformation = ContactInformation.Create(
            request.Website,
            request.Email,
            request.PhoneNumber);

        var marketInformation = MarketInformation.Create(
            request.IsMarketable,
            request.IsPublicCompany,
            request.MarketCap,
            request.StockSymbol);

        var issuer = Issuer.Create(
            request.Name,
            request.LegalName,
            request.RegistrationNumber,
            request.Type,
            address,
            contactInformation,
            marketInformation);

        var history = IssuerHistory.CreateSnapshot(
            issuer,
            IssuerHistoryAction.Created,
            _currentUserService.UserId,
            _currentUserService.UserName,
            "Issuer created");

        _context.Issuers.Add(issuer);
        _context.IssuerHistories.Add(history);

        await _context.SaveChangesAsync(cancellationToken);

        return issuer.Id;
    }
}