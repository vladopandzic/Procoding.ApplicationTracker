﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetOneJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplicationSource;

public class GetOneJobApplicationSourceEndpoint : Ardalis.ApiEndpoints.EndpointBaseAsync.WithRequest<Guid>.WithResult<JobApplicationSourceResponseDTO>
{
    private readonly IMediator _mediator;

    public GetOneJobApplicationSourceEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("job-application-sources/{id}")]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate")]

    public override Task<JobApplicationSourceResponseDTO> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new GetOneJobApplicationSourceQuery(id), cancellationToken);
    }
}
