using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplicationSource;

public class UpdateJobApplicationSourceEndpoint : EndpointBaseAsync.WithRequest<JobApplicationSourceUpdateRequestDTO>.WithResult<JobApplicationSourceUpdatedResponseDTO>
{

    private readonly ISender _sender;

    public UpdateJobApplicationSourceEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("job-application-sources")]
    public override Task<JobApplicationSourceUpdatedResponseDTO> HandleAsync(JobApplicationSourceUpdateRequestDTO request,
                                                                              CancellationToken cancellationToken = default)
    {
        return _sender.Send(new UpdateJobApplicationSourceCommand(request.Id,request.Name), cancellationToken);
    }
}
