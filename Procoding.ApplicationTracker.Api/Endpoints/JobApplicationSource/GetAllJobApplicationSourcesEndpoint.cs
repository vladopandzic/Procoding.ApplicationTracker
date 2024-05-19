using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.DTOs.Response;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplicationSource;

public class GetAllJobApplicationSourcesEndpoint : Ardalis.ApiEndpoints.EndpointBaseAsync.WithoutRequest.WithResult<JobApplicationSourceListResponseDTO>
{
    private readonly IMediator _mediator;

    public GetAllJobApplicationSourcesEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("job-application-sources")]
    public override Task<JobApplicationSourceListResponseDTO> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _mediator.Send(new Application.JobApplicationSources.Query.GetJobApplicationSources.GetJobApplicationSourcesQuery(), cancellationToken);
    }
}
