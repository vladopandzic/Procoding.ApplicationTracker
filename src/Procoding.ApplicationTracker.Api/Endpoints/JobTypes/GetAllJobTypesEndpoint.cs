using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.JobTypes.Queries.GetAllJobTypes;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobTypes;

public class GetAllJobTypesEndpoint : EndpointBaseAsync.WithoutRequest.WithResult<JobTypeListResponseDTO>
{

    readonly ISender _sender;
    public GetAllJobTypesEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("job-types")]
    public override Task<JobTypeListResponseDTO> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetAllJobTypesQuery());
    }
}
