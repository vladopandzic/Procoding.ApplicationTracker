using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.JobApplications.Commands.ApplyForJob;
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
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate")]
    public override async Task<IActionResult> HandleAsync(JobApplicationInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new ApplyForJobCommand(candidateId: request.CandidateId,
                                                               companyId: request.CompanyId,
                                                               jobApplicationSourceId: request.JobApplicationSourceId,
                                                               jobPositionTitle: request.JobPositionTitle,
                                                               jobAdLink: request.JobAdLink,
                                                               jobType: request.JobType.Value,
                                                               workLocationType: request.WorkLocationType.Value,
                                                               description: request.Description),
                                        cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
    }
}
