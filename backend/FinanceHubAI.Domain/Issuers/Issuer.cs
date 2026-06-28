using FinanceHubAI.Domain.Common;
using FinanceHubAI.Domain.Enums;
using FinanceHubAI.Domain.ValueObjects;

namespace FinanceHubAI.Domain.Entities;

public class Issuer : AuditableEntity
{
    public string Name { get; private set; } = null!;
    public string LegalName { get; private set; } = null!;
    public string RegistrationNumber { get; private set; } = null!;

    public IssuerType Type { get; private set; }
    public IssuerStatus Status { get; private set; }

    public Address Address { get; private set; } = null!;
    public ContactInformation ContactInformation { get; private set; } = null!;
    public MarketInformation MarketInformation { get; private set; } = null!;

    private Issuer()
    {
    }

    private Issuer(
        string name,
        string legalName,
        string registrationNumber,
        IssuerType type,
        Address address,
        ContactInformation contactInformation,
        MarketInformation marketInformation)
    {
        Name = name;
        LegalName = legalName;
        RegistrationNumber = registrationNumber;
        Type = type;
        Status = IssuerStatus.Active;
        Address = address;
        ContactInformation = contactInformation;
        MarketInformation = marketInformation;

        Validate();
    }

    public static Issuer Create(
        string name,
        string legalName,
        string registrationNumber,
        IssuerType type,
        Address address,
        ContactInformation contactInformation,
        MarketInformation marketInformation)
    {
        return new Issuer(
            name,
            legalName,
            registrationNumber,
            type,
            address,
            contactInformation,
            marketInformation);
    }

    public void UpdateDetails(
        string name,
        string legalName,
        IssuerType type,
        Address address,
        ContactInformation contactInformation,
        MarketInformation marketInformation)
    {
        Name = name;
        LegalName = legalName;
        Type = type;
        Address = address;
        ContactInformation = contactInformation;
        MarketInformation = marketInformation;

        MarkAsUpdated();
        Validate();
    }

    public void Activate()
    {
        Status = IssuerStatus.Active;
        MarkAsUpdated();
    }

    public void Suspend()
    {
        Status = IssuerStatus.Suspended;
        MarkAsUpdated();
    }

    public void Close()
    {
        Status = IssuerStatus.Closed;
        MarkAsUpdated();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException("Issuer name is required.");

        if (string.IsNullOrWhiteSpace(LegalName))
            throw new ArgumentException("Issuer legal name is required.");

        if (string.IsNullOrWhiteSpace(RegistrationNumber))
            throw new ArgumentException("Registration number is required.");

        if (Address is null)
            throw new ArgumentException("Address is required.");

        if (ContactInformation is null)
            throw new ArgumentException("Contact information is required.");

        if (MarketInformation is null)
            throw new ArgumentException("Market information is required.");
    }
}