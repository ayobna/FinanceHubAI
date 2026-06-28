using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinanceHubAI.Infrastructure.Persistence.Context;

public class FinanceHubDbContextFactory
    : IDesignTimeDbContextFactory<FinanceHubDbContext>
{
    public FinanceHubDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FinanceHubDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=AYOUN;Database=FinanceHubAI;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=True");

        return new FinanceHubDbContext(optionsBuilder.Options);
    }
}