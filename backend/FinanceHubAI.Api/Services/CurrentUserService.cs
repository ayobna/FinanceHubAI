using FinanceHubAI.Application.Common.Interfaces;

namespace FinanceHubAI.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public string UserId => "system";
    public string UserName => "System User";
}