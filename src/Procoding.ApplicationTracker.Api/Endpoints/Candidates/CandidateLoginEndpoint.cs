using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Employees.Commands.LoginEmployee;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class CandidateLoginEndpoint : EndpointBaseAsync.WithRequest<CandidateLoginRequestDTO>.WithResult<IActionResult>
{
    private readonly ISender _sender;

    public CandidateLoginEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("candidates/login")]
    [ProducesResponseType(typeof(CandidateLoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public override async Task<IActionResult> HandleAsync(CandidateLoginRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new LoginEmployeeCommand(request.Email, request.Password), cancellationToken);

        return result.Match<IActionResult>(Ok, err => err is Unauthorized401Exception ? Unauthorized(err.MapToResponse()) : BadRequest(err.MapToResponse()));
    }
}
