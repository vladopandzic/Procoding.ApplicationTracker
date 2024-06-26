﻿using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.Employees.Commands.InsertEmployee;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Api.Endpoints.Employees;

public class InsertEmployeeEndpoint : EndpointBaseAsync.WithRequest<EmployeeInsertRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;
    public InsertEmployeeEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost("employees")]
    [ProducesResponseType(typeof(EmployeeInsertedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate", Policy = Policies.EmployeeOnly)]
    public override async Task<IActionResult> HandleAsync(EmployeeInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new InsertEmployeeCommand(name: request.Name,
                                                                  surname: request.Surname,
                                                                  email: request.Email,
                                                                  password: request.Password),
                                        cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
    }
}