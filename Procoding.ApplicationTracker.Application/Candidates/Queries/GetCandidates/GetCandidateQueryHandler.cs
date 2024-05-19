using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Candidates.Queries.GetCandidates;

internal class GetCandidateQueryHandler : IQueryHandler<GetCandidatesQuery, CandidateListResponseDTO>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidateQueryHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CandidateListResponseDTO> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        var candidates = await _candidateRepository.GetCandidatesAsync(cancellationToken);

        var candidateDTOs = candidates.Select(x => new CandidateDTO(x.Name, x.Surname, x.Email.Value)).ToList();

        return new CandidateListResponseDTO(candidateDTOs.AsReadOnly());
    }
}
