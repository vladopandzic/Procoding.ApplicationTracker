namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplicationSource;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using System.Threading;
using System.Threading.Tasks;

public class InsertJobApplicationSourceEndpoint : EndpointBaseAsync.WithRequest<JobApplicationSourceInsertRequestDTO>.WithResult<JobApplicationSourceInsertedResponseDTO>
{

    private readonly ISender _sender;

    public InsertJobApplicationSourceEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("job-application-sources")]

    public override Task<JobApplicationSourceInsertedResponseDTO> HandleAsync(JobApplicationSourceInsertRequestDTO request,
                                                                              CancellationToken cancellationToken = default)
    {
        return _sender.Send(new AddJobApplicationSourceCommand(request.Name), cancellationToken);
    }
}
