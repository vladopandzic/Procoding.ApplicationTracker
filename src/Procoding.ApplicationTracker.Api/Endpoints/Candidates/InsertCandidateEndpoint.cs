using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class InsertCandidateEndpoint : EndpointBaseAsync.WithRequest<CandidateInsertRequestDTO>.WithResult<CandidateInsertedResponseDTO>
{
    readonly ISender _sender;
    public InsertCandidateEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpPost("candidates")]
    public override Task<CandidateInsertedResponseDTO> HandleAsync(CandidateInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new InsertCandidateCommand(request.Name, request.Surname, request.Email), cancellationToken);
    }
}
