using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.WorkLocationTypes.Queries.GetAllJobWorkTypes;
using Procoding.ApplicationTracker.DTOs.Response.WorkLocationTypes;

namespace Procoding.ApplicationTracker.Api.Endpoints.WorkLocationType;

public class GetAllWorkLocationTypesEndpoint : EndpointBaseAsync.WithoutRequest.WithResult<WorkLocationTypeListResponse>
{

    readonly ISender _sender;
    public GetAllWorkLocationTypesEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("work-location-types")]
    public override Task<WorkLocationTypeListResponse> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetAllWorkLocationTypesQuery());
    }
}
