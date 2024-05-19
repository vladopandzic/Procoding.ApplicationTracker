using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class UpdateCandidateEndpoint : EndpointBaseAsync.WithRequest<UpdateCandidateCommand>.WithResult<CandidateUpdatedResponseDTO>
{

    readonly ISender _sender;

    public UpdateCandidateEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPut("candidates")]

    public override Task<CandidateUpdatedResponseDTO> HandleAsync(UpdateCandidateCommand request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(request, cancellationToken);
    }
}
