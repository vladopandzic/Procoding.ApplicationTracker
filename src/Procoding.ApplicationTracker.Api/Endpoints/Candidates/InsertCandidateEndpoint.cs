using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class InsertCandidateEndpoint : EndpointBaseAsync.WithRequest<CandidateInsertRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;
    public InsertCandidateEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost("candidates")]
    [ProducesResponseType(typeof(CandidateInsertedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public override async Task<IActionResult> HandleAsync(CandidateInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new InsertCandidateCommand(request.Name, request.Surname, request.Email, request.Password), cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));

    }
}
