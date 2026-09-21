using FinanceHubAI.Api.Common;
using FinanceHubAI.Api.Contracts.Issuers;
using FinanceHubAI.Application.Issuers.Commands.CreateIssuer;
using FinanceHubAI.Application.Issuers.Queries.GetIssuerById;
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
            return BadRequest(ApiResponse<object>.Fail(result.Error.Code, result.Error.Message));
        }

        return CreatedAtAction(
           nameof(GetById),
           new { id = result.Value },
           ApiResponse<object>.Ok(new { id = result.Value }));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
    Guid id,
    CancellationToken cancellationToken)
    {
        var query = new GetIssuerByIdQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(
                ApiResponse<object>.Fail(
                    result.Error.Code,
                    result.Error.Message));
        }

        return Ok(ApiResponse<IssuerDto>.Ok(result.Value));
    }
}