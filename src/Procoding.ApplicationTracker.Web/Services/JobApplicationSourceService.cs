using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services;

public class JobApplicationSourceService : IJobApplicationSourceService
{
    private readonly HttpClient _httpClient;

    public JobApplicationSourceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<JobApplicationSourceListResponseDTO?> GetJobApplicationSourcesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<JobApplicationSourceListResponseDTO>(UrlConstants.JobApplicationSources.GET_ALL_URL);
    }

    public async Task<JobApplicationSourceResponseDTO?> GetJobApplicationSourceAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<JobApplicationSourceResponseDTO>(UrlConstants.JobApplicationSources.GetOne(id));
    }

    public async Task<JobApplicationSourceInsertedResponseDTO?> InsertJobApplicationSourceAsync(JobApplicationSourceInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(UrlConstants.JobApplicationSources.InsertUrl(), request, cancellationToken);

        return await response.Content.ReadFromJsonAsync<JobApplicationSourceInsertedResponseDTO?>(cancellationToken);
    }

    public async Task<JobApplicationSourceUpdatedResponseDTO?> UpdateJobApplicationSourceAsync(JobApplicationSourceUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(UrlConstants.JobApplicationSources.UpdateUrl(), request, cancellationToken);

        return await response.Content.ReadFromJsonAsync<JobApplicationSourceUpdatedResponseDTO?>(cancellationToken);

    }
}
