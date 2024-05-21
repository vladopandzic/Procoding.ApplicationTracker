using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Api.Endpoints.Companies;

public class UpdateCompanyEndpoint : EndpointBaseAsync.WithRequest<CompanyUpdateRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;

    public UpdateCompanyEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPut("companies")]
    [ProducesResponseType(typeof(CompanyUpdatedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public override async Task<IActionResult> HandleAsync(CompanyUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new UpdateCompanyCommand(request.Id, request.Name, request.OfficialWebSiteLink), cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));

    }
}
