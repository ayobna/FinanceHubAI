using FinanceHubAI.Application.Common.Interfaces;
using FinanceHubAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceHubAI.Infrastructure.Persistence.Context;

public class FinanceHubDbContext : DbContext, IFinanceHubDbContext
{
    public FinanceHubDbContext(DbContextOptions<FinanceHubDbContext> options)
        : base(options)
    {
    }

    public DbSet<Issuer> Issuers => Set<Issuer>();
    public DbSet<IssuerHistory> IssuerHistories => Set<IssuerHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinanceHubDbContext).Assembly);
    }
}