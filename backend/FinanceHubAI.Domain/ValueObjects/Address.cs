namespace FinanceHubAI.Domain.ValueObjects;

public sealed class Address
{
    public string Country { get; private set; } = null!;
    public string City { get; private set; } = null!;
    public string Street { get; private set; } = null!;

    private Address() { }

    private Address(string country, string city, string street)
    {
        Country = country;
        City = city;
        Street = street;

        Validate();
    }

    public static Address Create(string country, string city, string street)
    {
        return new Address(country, city, street);
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Country))
            throw new ArgumentException("Country is required.");

        if (string.IsNullOrWhiteSpace(City))
            throw new ArgumentException("City is required.");

        if (string.IsNullOrWhiteSpace(Street))
            throw new ArgumentException("Street is required.");
    }
}