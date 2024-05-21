using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Exceptions;
using Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class UpdateCandidateEndpoint : EndpointBaseAsync.WithRequest<CandidateUpdateRequestDTO>.WithResult<IActionResult>
{

    readonly ISender _sender;

    public UpdateCandidateEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPut("candidates")]
    [ProducesResponseType(typeof(CandidateUpdatedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public override async Task<IActionResult> HandleAsync(CandidateUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new UpdateCandidateCommand(request.Id, request.Name, request.Surname, request.Email), cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));

    }
}
