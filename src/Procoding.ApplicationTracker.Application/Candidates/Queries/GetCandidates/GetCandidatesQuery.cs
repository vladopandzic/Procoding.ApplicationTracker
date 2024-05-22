using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Queries.GetCandidates;

public sealed class GetCandidatesQuery : IQuery<CandidateListResponseDTO>
{
    public GetCandidatesQuery(int? pageNumber, int? pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int? PageNumber { get; set; }

    public int? PageSize { get; set; }
}
