using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services;

public class CandidateService : ICandidateService
{
    private readonly HttpClient _httpClient;

    public CandidateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<CandidateResponseDTO>> GetCandidateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(UrlConstants.Candidates.GetOne(id));

        return await response.HandleResponseAsync<CandidateResponseDTO>(cancellationToken);
    }

    public async Task<Result<CandidateListResponseDTO>> GetCandidatesAsync(EmployeeGetListRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync($"{UrlConstants.Candidates.GET_ALL_URL}?{request.ToQueryString()}");

        return await response.HandleResponseAsync<CandidateListResponseDTO>(cancellationToken);
    }

    public async Task<Result<CandidateInsertedResponseDTO>> InsertCandidateAsync(CandidateInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(UrlConstants.Candidates.InsertUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<CandidateInsertedResponseDTO>(cancellationToken);
    }

    public async Task<Result<CandidateUpdatedResponseDTO>> UpdateCandidateAsync(CandidateUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(UrlConstants.Candidates.UpdateUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<CandidateUpdatedResponseDTO>(cancellationToken);
    }
}
