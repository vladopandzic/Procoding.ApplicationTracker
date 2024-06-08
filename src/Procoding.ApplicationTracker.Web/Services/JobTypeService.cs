using FluentResults;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services;

public class JobTypeService : IJobTypeService
{
    private readonly HttpClient _httpClient;

    public JobTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<JobTypeListResponseDTO>> GetJobTypesAsync(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(UrlConstants.JobTypes.GET_ALL_URL);

        return await response.HandleResponseAsync<JobTypeListResponseDTO>(cancellationToken);
    }
}
