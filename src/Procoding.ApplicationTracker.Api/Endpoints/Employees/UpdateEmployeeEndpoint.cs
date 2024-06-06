﻿using Ardalis.ApiEndpoints;
using MediatR;
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
    public override async Task<IActionResult> HandleAsync(EmployeeUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new UpdateEmployeeCommand(request.Id, request.Name, request.Surname, request.Email), cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));

    }
}