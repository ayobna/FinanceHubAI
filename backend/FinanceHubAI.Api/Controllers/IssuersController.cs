using FinanceHubAI.Api.Common;
using FinanceHubAI.Api.Contracts.Issuers;
using FinanceHubAI.Application.Issuers.Commands.CreateIssuer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceHubAI.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IssuersController : ControllerBase
{
    private readonly ISender _sender;

    public IssuersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateIssuerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateIssuerCommand(
            request.Name,
            request.LegalName,
            request.RegistrationNumber,
            request.Type,
            request.Country,
            request.City,
            request.Street,
            request.Website,
            request.Email,
            request.PhoneNumber,
            request.IsMarketable,
            request.IsPublicCompany,
            request.MarketCap,
            request.StockSymbol);


        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(ApiResponse<object>.Fail( result.Error.Code, result.Error.Message));
        }

        return CreatedAtAction(
            nameof(Create),
            new { id = result.Value },
            ApiResponse<object>.Ok(new { id = result.Value }));
    }
}