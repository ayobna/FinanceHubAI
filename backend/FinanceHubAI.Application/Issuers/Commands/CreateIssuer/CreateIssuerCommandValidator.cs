using FluentValidation;

namespace FinanceHubAI.Application.Issuers.Commands.CreateIssuer;

public sealed class CreateIssuerCommandValidator
    : AbstractValidator<CreateIssuerCommand>
{
    public CreateIssuerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.LegalName)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.RegistrationNumber)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Country)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.City)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Street)
            .NotEmpty()
            .MaximumLength(250);

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Website)
            .MaximumLength(250);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(50);

        RuleFor(x => x.StockSymbol)
            .NotEmpty()
            .When(x => x.IsPublicCompany);

        RuleFor(x => x.MarketCap)
            .GreaterThanOrEqualTo(0)
            .When(x => x.MarketCap.HasValue);
    }
}