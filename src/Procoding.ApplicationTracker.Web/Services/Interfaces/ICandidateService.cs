using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface ICandidateService
{
    /// <summary>
    /// Gets candidate by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CandidateResponseDTO?> GetCandidateAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all candidates.
    /// </summary>
    /// <returns></returns>
    Task<CandidateListResponseDTO?> GetCandidatesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts one candidate.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CandidateInsertedResponseDTO?> InsertCandidateAsync(CandidateInsertRequestDTO request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates one candidate.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CandidateUpdatedResponseDTO?> UpdateCandidateAsync(CandidateUpdateRequestDTO request, CancellationToken cancellationToken = default);
}
