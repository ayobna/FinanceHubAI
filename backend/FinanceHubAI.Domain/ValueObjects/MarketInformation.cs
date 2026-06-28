namespace FinanceHubAI.Domain.ValueObjects;

public sealed class MarketInformation
{
    public bool IsMarketable { get; private set; }
    public bool IsPublicCompany { get; private set; }
    public decimal? MarketCap { get; private set; }
    public string? StockSymbol { get; private set; }

    private MarketInformation() { }

    private MarketInformation(
        bool isMarketable,
        bool isPublicCompany,
        decimal? marketCap,
        string? stockSymbol)
    {
        IsMarketable = isMarketable;
        IsPublicCompany = isPublicCompany;
        MarketCap = marketCap;
        StockSymbol = stockSymbol;

        Validate();
    }

    public static MarketInformation Create(
        bool isMarketable,
        bool isPublicCompany,
        decimal? marketCap,
        string? stockSymbol)
    {
        return new MarketInformation(
            isMarketable,
            isPublicCompany,
            marketCap,
            stockSymbol);
    }

    private void Validate()
    {
        if (IsPublicCompany && string.IsNullOrWhiteSpace(StockSymbol))
            throw new ArgumentException("Public company must have a stock symbol.");

        if (MarketCap is < 0)
            throw new ArgumentException("Market cap cannot be negative.");
    }
}