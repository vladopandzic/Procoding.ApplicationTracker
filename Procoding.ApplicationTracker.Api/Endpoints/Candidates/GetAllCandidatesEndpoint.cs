using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application.Candidates.Queries.GetCandidates;
using Procoding.ApplicationTracker.Application.Companies.Queries.GetCompanies;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class GetAllCandidatesEndpoint : EndpointBaseAsync.WithoutRequest.WithResult<CandidateListResponseDTO>
{
    readonly ISender _sender;
    public GetAllCandidatesEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("candidates")]
    public override Task<CandidateListResponseDTO> HandleAsync(CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetCandidatesQuery(), cancellationToken);
    }
}
