using FluentResults;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response.WorkLocationTypes;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services;

public class WorkLocationTypeService : IWorkLocationTypeService
{
    private readonly HttpClient _httpClient;

    public WorkLocationTypeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<WorkLocationTypeListResponse>> GetWorkLocationTypesAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetAsync(UrlConstants.WorkLocationTypes.GET_ALL_URL);

        return await response.HandleResponseAsync<WorkLocationTypeListResponse>(cancellationToken);
    }
}
