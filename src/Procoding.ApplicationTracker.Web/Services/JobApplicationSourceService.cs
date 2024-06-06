using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace Procoding.ApplicationTracker.Web.Services;

public class JobApplicationSourceService : IJobApplicationSourceService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;

    public JobApplicationSourceService(HttpClient httpClient, ITokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<JobApplicationSourceListResponseDTO>> GetJobApplicationSourcesAsync(CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.GetAsync(UrlConstants.JobApplicationSources.GET_ALL_URL);

        return await response.HandleResponseAsync<JobApplicationSourceListResponseDTO>(cancellationToken);
    }

    public async Task<Result<JobApplicationSourceResponseDTO>> GetJobApplicationSourceAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.GetAsync(UrlConstants.JobApplicationSources.GetOne(id));

        return await response.HandleResponseAsync<JobApplicationSourceResponseDTO>(cancellationToken);
    }

    public async Task<Result<JobApplicationSourceInsertedResponseDTO>> InsertJobApplicationSourceAsync(JobApplicationSourceInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.PostAsJsonAsync(UrlConstants.JobApplicationSources.InsertUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<JobApplicationSourceInsertedResponseDTO>(cancellationToken);
    }

    public async Task<Result<JobApplicationSourceUpdatedResponseDTO>> UpdateJobApplicationSourceAsync(JobApplicationSourceUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.PutAsJsonAsync(UrlConstants.JobApplicationSources.UpdateUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<JobApplicationSourceUpdatedResponseDTO>(cancellationToken);
    }

    private async Task Authorize()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAccessTokenAsync());
    }
}
