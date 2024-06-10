using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Queries.GetOneCandidate;

internal sealed class GetOneCandidateQueryHandler : IQueryHandler<GetOneCandidateQuery, CandidateResponseDTO>
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

        var companyDTO = new CandidateDTO(candidate.Id, candidate.Name, candidate.Surname, candidate.Email.Value);

        return new CandidateResponseDTO(companyDTO);
    }
}
