using FinanceHubAI.Application.Common.Responses;
using MediatR;

namespace FinanceHubAI.Application.Issuers.Queries.GetIssuerById;

public sealed record GetIssuerByIdQuery(Guid Id)
    : IRequest<Result<IssuerDto>>;