using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services;

public class CandidateService : ICandidateService
{
    private readonly HttpClient _httpClient;

    public CandidateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CandidateResponseDTO?> GetCandidateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<CandidateResponseDTO>(UrlConstants.Candidates.GetOne(id));
    }

    public async Task<CandidateListResponseDTO?> GetCandidatesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<CandidateListResponseDTO>(UrlConstants.Candidates.GET_ALL_URL);
    }

    public async Task<CandidateInsertedResponseDTO?> InsertCandidateAsync(CandidateInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(UrlConstants.Candidates.InsertUrl(), request, cancellationToken);

        return await response.Content.ReadFromJsonAsync<CandidateInsertedResponseDTO?>(cancellationToken);
    }

    public async Task<CandidateUpdatedResponseDTO?> UpdateCandidateAsync(CandidateUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(UrlConstants.Candidates.UpdateUrl(), request, cancellationToken);

        return await response.Content.ReadFromJsonAsync<CandidateUpdatedResponseDTO?>(cancellationToken);
    }
}
