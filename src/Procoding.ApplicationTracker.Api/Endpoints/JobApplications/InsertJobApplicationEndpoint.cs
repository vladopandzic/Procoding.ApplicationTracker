﻿using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplications;

public class InsertJobApplicationEndpoint : EndpointBaseAsync.WithRequest<JobApplicationInsertRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;
    public InsertJobApplicationEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost("job-applications")]
    [ProducesResponseType(typeof(JobApplicationInsertedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public override async Task<IActionResult> HandleAsync(JobApplicationInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        //var result = await _sender.Send(new JobApplicationInsertCommand(request.Name, request.OfficialWebSiteLink), cancellationToken);

        //return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
        throw new NotImplementedException();

    }
}
