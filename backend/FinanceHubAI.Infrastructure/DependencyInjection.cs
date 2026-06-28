using FinanceHubAI.Application.Common.Interfaces;
using FinanceHubAI.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceHubAI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<FinanceHubDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IFinanceHubDbContext>(provider =>
            provider.GetRequiredService<FinanceHubDbContext>());

        return services;
    }
}
