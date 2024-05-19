using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Candidates.Queries.GetOneCandidate;
using Procoding.ApplicationTracker.Application.Companies.Queries.GetOneCompany;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api;

public class GetOneCandidateEndpoint : EndpointBaseAsync.WithRequest<Guid>.WithResult<CandidateResponseDTO>
{
    readonly ISender _sender;
    public GetOneCandidateEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("candidates/{id}")]

    public override Task<CandidateResponseDTO> HandleAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetOneCandidateQuery(id), cancellationToken);
    }
}
