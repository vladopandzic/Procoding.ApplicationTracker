using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Employees.Commands.UpdateEmployee;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Api.Endpoints.Employees;

public class UpdateEmployeeEndpoint : EndpointBaseAsync.WithRequest<EmployeeUpdateRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;

    public UpdateEmployeeEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPut("employees")]
    [ProducesResponseType(typeof(EmployeeUpdatedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate", Policy = Policies.EmployeeOnly)]
    public override async Task<IActionResult> HandleAsync(EmployeeUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new UpdateEmployeeCommand(id: request.Id,
                                                                  name: request.Name,
                                                                  surname: request.Surname,
                                                                  email: request.Email,
                                                                  password: request.Password,
                                                                  updatePassword: request.UpdatePassword),
                                        cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
    }
}
