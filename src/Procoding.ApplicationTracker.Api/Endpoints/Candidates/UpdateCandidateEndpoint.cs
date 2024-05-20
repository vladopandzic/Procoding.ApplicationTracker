using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class UpdateCandidateEndpoint : EndpointBaseAsync.WithRequest<CandidateUpdateRequestDTO>.WithResult<CandidateUpdatedResponseDTO>
{

    readonly ISender _sender;

    public UpdateCandidateEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPut("candidates")]

    public override Task<CandidateUpdatedResponseDTO> HandleAsync(CandidateUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new UpdateCandidateCommand(request.Id,request.Name, request.Surname, request.Email), cancellationToken);
    }
}
