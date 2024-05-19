using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Candidates.Queries.GetOneCandidate;

public class GetOneCandidateQuery : IQuery<CandidateResponseDTO>
{
    public GetOneCandidateQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
