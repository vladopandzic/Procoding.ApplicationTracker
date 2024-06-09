using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.JobApplications.Commands.UpdateJobApplication;
using Procoding.ApplicationTracker.Domain.Auth;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplications;

public class UpdateJobApplicationEndpoint : EndpointBaseAsync.WithRequest<JobApplicationUpdateRequestDTO>.WithResult<IActionResult>
{
    readonly ISender _sender;
    private readonly IIdentityContext _identityContext;

    public UpdateJobApplicationEndpoint(ISender sender, IIdentityContext identityContext)
    {
        _sender = sender;
        _identityContext = identityContext;
    }

    [HttpPut("job-applications")]
    [ProducesResponseType(typeof(JobApplicationUpdatedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate", Policy = Policies.CandidateOnly)]

    public override async Task<IActionResult> HandleAsync(JobApplicationUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
      
        var result = await _sender.Send(new UpdateJobApplicationCommand(id: request.Id,
                                                                        candidateId: _identityContext.UserId!.Value,
                                                                        companyId: request.CompanyId,
                                                                        jobApplicationSourceId: request.JobApplicationSourceId,
                                                                        jobPositionTitle: request.JobPositionTitle,
                                                                        workLocationType: request.WorkLocationType.Value,
                                                                        jobType: request.JobType.Value,
                                                                        jobAdLink: request.JobAdLink,
                                                                        description: request.Description),
                                        cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
    }
}
