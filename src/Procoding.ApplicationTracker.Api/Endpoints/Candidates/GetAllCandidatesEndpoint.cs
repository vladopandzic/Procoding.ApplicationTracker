using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Procoding.ApplicationTracker.Application;
using Procoding.ApplicationTracker.Application.Candidates.Queries.GetCandidates;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Api.Endpoints.Candidates;

public class GetAllCandidatesEndpoint : EndpointBaseAsync.WithRequest<CandidateGetListRequestDTO>.WithResult<CandidateListResponseDTO>
{
    readonly ISender _sender;
    public GetAllCandidatesEndpoint(ISender sender)
    {
        this._sender = sender;
    }

    [HttpGet("candidates")]

    public override Task<CandidateListResponseDTO> HandleAsync([FromQuery] CandidateGetListRequestDTO request, CancellationToken cancellationToken = default)
    {
        return _sender.Send(new GetCandidatesQuery(pageNumber: request.PageNumber,
                                                   pageSize: request.PageSize,
                                                   filters: request.Filters.Select(x => new Filter()
                                                   {
                                                       Key = x.Key,
                                                       Operator = x.Operator,
                                                       Value = x.Value
                                                   }).ToList(),
                                                   sort: request.Sort.Select(x => new Sort()
                                                   {
                                                       SortBy = x.SortBy,
                                                       Descending = x.Descending
                                                   }).ToList()), cancellationToken);
    }
}
