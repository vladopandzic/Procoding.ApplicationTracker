using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services;

public class CompanyService : ICompanyService
{
    private readonly HttpClient _httpClient;

    public CompanyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CompanyListResponseDTO?> GetCompaniesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<CompanyListResponseDTO>(UrlConstants.Companies.GET_ALL_URL);
    }

    public async Task<CompanyResponseDTO?> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<CompanyResponseDTO>(UrlConstants.Companies.GetOne(id));
    }

    public async Task<CompanyInsertedResponseDTO?> InsertCompanyAsync(CompanyInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(UrlConstants.Companies.InsertUrl(), request, cancellationToken);

        return await response.Content.ReadFromJsonAsync<CompanyInsertedResponseDTO?>(cancellationToken);
    }

    public async Task<CompanyUpdatedResponseDTO?> UpdateCompanyAsync(CompanyUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(UrlConstants.Companies.UpdateUrl(), request, cancellationToken);

        return await response.Content.ReadFromJsonAsync<CompanyUpdatedResponseDTO?>(cancellationToken);
    }
}
