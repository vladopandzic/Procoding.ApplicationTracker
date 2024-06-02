using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Employees.Commands.LoginEmployee;
using Procoding.ApplicationTracker.Application.Employees.Commands.RefreshLoginTokenForEmployee;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Api.Endpoints.Employees;

public class EmployeeRefreshTokenEndpoint : EndpointBaseAsync.WithRequest<TokenRequestDTO>.WithResult<IActionResult>
{
    private readonly ISender _sender;

    public EmployeeRefreshTokenEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("employees/login/refresh")]
    [ProducesResponseType(typeof(EmployeeLoginResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public override async Task<IActionResult> HandleAsync(TokenRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new RefreshLoginTokenForEmployeeCommand(request.AccessToken, request.RefreshToken), cancellationToken);

        return result.Match<IActionResult>(Ok, err => err is Unauthorized401Exception ? Unauthorized(err.MapToResponse()) : BadRequest(err.MapToResponse()));
    }
}
