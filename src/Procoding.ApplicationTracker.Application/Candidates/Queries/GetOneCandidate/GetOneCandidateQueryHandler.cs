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

namespace Procoding.ApplicationTracker.Application.Candidates.Queries.GetOneCandidate;

public class GetOneCandidateQueryHandler : IQueryHandler<GetOneCandidateQuery, CandidateResponseDTO>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetOneCandidateQueryHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CandidateResponseDTO> Handle(GetOneCandidateQuery request, CancellationToken cancellationToken)
    {
        var candidate = await _candidateRepository.GetCandidateAsync(request.Id, cancellationToken);

        if (candidate is null)
            throw new Domain.Exceptions.CandidateDoesNotExistException();

        var companyDTO = new CandidateDTO(candidate.Name, candidate.Surname, candidate.Email.Value);

        return new CandidateResponseDTO(companyDTO);
    }
}
