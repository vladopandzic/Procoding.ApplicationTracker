using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.JobApplications.Commands.ApplyForJob;
using Procoding.ApplicationTracker.Application.JobApplications.Commands.UpdateJobApplication;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplications;

public class UpdateJobApplicationEndpoint : EndpointBaseAsync.WithRequest<JobApplicationUpdateRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;
    public UpdateJobApplicationEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPut("job-applications")]
    [ProducesResponseType(typeof(JobApplicationUpdatedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate")]

    public override async Task<IActionResult> HandleAsync(JobApplicationUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new UpdateJobApplicationCommand(id: request.Id, 
                                                                        candidateId: request.CandidateId,
                                                                        companyId: request.CompanyId,
                                                                        jobApplicationSourceId: request.JobApplicationSourceId),
                                      cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
    }
}
