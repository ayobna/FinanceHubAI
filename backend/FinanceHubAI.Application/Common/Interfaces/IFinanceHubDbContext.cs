using FinanceHubAI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinanceHubAI.Application.Common.Interfaces;

public interface IFinanceHubDbContext
{
    DbSet<Issuer> Issuers { get; }
    DbSet<IssuerHistory> IssuerHistories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}