using FluentResults;
using Microsoft.AspNetCore.Components.Authorization;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace Procoding.ApplicationTracker.Web.Services;

public class CandidateService : ICandidateService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationState;
    private readonly ITokenProvider _tokenProvider;

    public CandidateService(HttpClient httpClient, AuthenticationStateProvider authenticationState)
    {
        _httpClient = httpClient;
        _authenticationState = authenticationState;
    }

    public async Task<Result<CandidateResponseDTO>> GetCandidateAsync(Guid id, CancellationToken cancellationToken = default)
    {
      

        var response = await _httpClient.GetAsync(UrlConstants.Candidates.GetOne(id));

        return await response.HandleResponseAsync<CandidateResponseDTO>(cancellationToken);
    }

    public async Task<Result<CandidateListResponseDTO>> GetCandidatesAsync(EmployeeGetListRequestDTO request, CancellationToken cancellationToken = default)
    {
        var authState = await _authenticationState.GetAuthenticationStateAsync();
        var token = authState.User.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
