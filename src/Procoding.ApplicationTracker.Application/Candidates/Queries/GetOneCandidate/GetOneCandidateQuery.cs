using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Queries.GetOneCandidate;

public sealed class GetOneCandidateQuery : IQuery<CandidateResponseDTO>
{
    public GetOneCandidateQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
