using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Queries.GetCandidates;

internal sealed class GetCandidateQueryHandler : IQueryHandler<GetCandidatesQuery, CandidateListResponseDTO>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidateQueryHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<CandidateListResponseDTO> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        var specification = new Specifications.CandidateGetListSpecification(request.PageNumber, request.PageSize);

        var candidates = await _candidateRepository.GetCandidatesAsync(specification, cancellationToken);
        var count = await _candidateRepository.CountAsync(specification, cancellationToken);

        var candidateDTOs = candidates.Select(x => new CandidateDTO(x.Id, x.Name, x.Surname, x.Email.Value)).ToList();

        return new CandidateListResponseDTO(candidates: candidateDTOs.AsReadOnly(), totalCount: count);
    }
}
