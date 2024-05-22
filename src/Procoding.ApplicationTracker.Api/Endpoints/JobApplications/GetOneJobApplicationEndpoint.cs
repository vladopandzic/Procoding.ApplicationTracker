using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Candidates.Queries.GetOneCandidate;
using Procoding.ApplicationTracker.Application.JobApplications.Queries.GetOneJobApplication;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplications;

public class GetOneJobApplicationEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithResult<JobApplicationResponseDTO>
{
    readonly ISender _sender;
    public GetOneJobApplicationEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("job-applications/{id}")]

    public override Task<JobApplicationResponseDTO> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetOneJobApplicationQuery(id), cancellationToken);
    }
}
