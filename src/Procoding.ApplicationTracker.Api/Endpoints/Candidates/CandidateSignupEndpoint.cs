using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Candidates.Commands.SignupCandidate;
using Procoding.ApplicationTracker.Application.Employees.Commands.LoginEmployee;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class CandidateSignupEndpoint : EndpointBaseAsync.WithRequest<CandidateSignupRequestDTO>.WithResult<IActionResult>
{
    private readonly ISender _sender;

    public CandidateSignupEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("candidates/signup")]
    [ProducesResponseType(typeof(CandidateLoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public override async Task<IActionResult> HandleAsync(CandidateSignupRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new SignupCandidateCommand(email: request.Email,
                                                                   password: request.Password,
                                                                   name: request.Name,
                                                                   surname: request.Surname),
                                        cancellationToken);

        return result.Match<IActionResult>(Ok, err => err is Unauthorized401Exception ? Unauthorized(err.MapToResponse()) : BadRequest(err.MapToResponse()));
    }
}
