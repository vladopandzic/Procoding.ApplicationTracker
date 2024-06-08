namespace Procoding.ApplicationTracker.Api.Endpoints.JobApplicationSource;
using Ardalis.ApiEndpoints;
using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using System.Threading;
using System.Threading.Tasks;

public class InsertJobApplicationSourceEndpoint : EndpointBaseAsync.WithRequest<JobApplicationSourceInsertRequestDTO>.WithResult<IActionResult>
{
    private readonly ISender _sender;

    public InsertJobApplicationSourceEndpoint(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("job-application-sources")]
    [ProducesResponseType(typeof(JobApplicationSourceInsertedResponseDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [Authorize(AuthenticationSchemes = "BearerEmployee,BearerCandidate", Policy = Policies.EmployeeOnly)]

    public override async Task<IActionResult> HandleAsync(JobApplicationSourceInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var result = await _sender.Send(new AddJobApplicationSourceCommand(request.Name), cancellationToken);

        return result.Match<IActionResult>(Ok, err => BadRequest(err.MapToResponse()));
    }
}
