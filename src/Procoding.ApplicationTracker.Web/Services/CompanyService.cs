using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace Procoding.ApplicationTracker.Web.Services;

public class CompanyService : ICompanyService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;

    public CompanyService(HttpClient httpClient, ITokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;
    }

    public async Task<Result<CompanyListResponseDTO>> GetCompaniesAsync(CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.GetAsync(UrlConstants.Companies.GET_ALL_URL);

        return await response.HandleResponseAsync<CompanyListResponseDTO>(cancellationToken);

    }

    public async Task<Result<CompanyResponseDTO>> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.GetAsync(UrlConstants.Companies.GetOne(id));

        return await response.HandleResponseAsync<CompanyResponseDTO>(cancellationToken);
    }

    public async Task<Result<CompanyInsertedResponseDTO>> InsertCompanyAsync(CompanyInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.PostAsJsonAsync(UrlConstants.Companies.InsertUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<CompanyInsertedResponseDTO>(cancellationToken);
    }

    public async Task<Result<CompanyUpdatedResponseDTO>> UpdateCompanyAsync(CompanyUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.PutAsJsonAsync(UrlConstants.Companies.UpdateUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<CompanyUpdatedResponseDTO>(cancellationToken);
    }

    private async Task Authorize()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAccessTokenAsync());
    }
}
