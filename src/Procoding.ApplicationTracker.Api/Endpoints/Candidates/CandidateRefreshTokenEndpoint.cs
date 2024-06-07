using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Candidates.Commands.RefreshLoginTokenForCandidate;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class CandidateRefreshTokenEndpoint : EndpointBaseAsync.WithRequest<TokenRequestDTO>.WithResult<IActionResult>
{
    private readonly ISender _sender;

    public CandidateRefreshTokenEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("candidates/login/refresh")]
    [ProducesResponseType(typeof(CandidateLoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public override async Task<IActionResult> HandleAsync(TokenRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new RefreshLoginTokenForCandidateCommand(request.AccessToken, request.RefreshToken), cancellationToken);

        return result.Match<IActionResult>(Ok, err => err is Unauthorized401Exception ? Unauthorized(err.MapToResponse()) : BadRequest(err.MapToResponse()));
    }
}
