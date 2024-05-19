namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplicationSource;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Response;
using System.Threading;
using System.Threading.Tasks;

public class InsertJobApplicationSourceEndpoint : EndpointBaseAsync.WithRequest<AddJobApplicationSourceCommand>.WithResult<JobApplicationSourceInsertedResponseDTO>
{

    private readonly ISender _sender;

    public InsertJobApplicationSourceEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("job-application-sources")]

    public override Task<JobApplicationSourceInsertedResponseDTO> HandleAsync(AddJobApplicationSourceCommand request,
                                                                              CancellationToken cancellationToken = default)
    {
        return _sender.Send(request, cancellationToken);
    }
}
