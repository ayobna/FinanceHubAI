namespace FinanceHubAI.Domain.ValueObjects;

public sealed class ContactInformation
{
    public string? Website { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }

    private ContactInformation() { }

    private ContactInformation(string? website, string? email, string? phoneNumber)
    {
        Website = website;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static ContactInformation Create(
        string? website,
        string? email,
        string? phoneNumber)
    {
        return new ContactInformation(website, email, phoneNumber);
    }
}