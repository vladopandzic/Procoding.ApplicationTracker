using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplicationSource;

public class UpdateJobApplicationSourceEndpoint : EndpointBaseAsync.WithRequest<JobApplicationSourceUpdateRequestDTO>.WithResult<IActionResult>
{

    private readonly ISender _sender;

    public UpdateJobApplicationSourceEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("job-application-sources")]
    [ProducesResponseType(typeof(JobApplicationSourceUpdatedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate")]

    public override async Task<IActionResult> HandleAsync(JobApplicationSourceUpdateRequestDTO request,
                                                          CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new UpdateJobApplicationSourceCommand(request.Id, request.Name), cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));

    }
}
