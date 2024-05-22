using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Api.Endpoints.Companies;

public class InsertCompanyEndpoint : EndpointBaseAsync.WithRequest<CompanyInsertRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;
    public InsertCompanyEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost("companies")]
    [ProducesResponseType(typeof(CompanyInsertedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public override async Task<IActionResult> HandleAsync(CompanyInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new InsertCompanyCommand(request.Name, request.OfficialWebSiteLink), cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
    }
}
