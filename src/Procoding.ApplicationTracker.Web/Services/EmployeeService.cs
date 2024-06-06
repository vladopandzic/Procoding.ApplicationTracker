using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using System.Net.Http.Headers;

namespace Procoding.ApplicationTracker.Web.Services;

public class EmployeeService : IEmployeeService
{
    private readonly HttpClient _httpClient;
    private readonly ITokenProvider _tokenProvider;

    public EmployeeService(HttpClient httpClient, ITokenProvider tokenProvider)
    {
        _httpClient = httpClient;
        _tokenProvider = tokenProvider;

    }

    public async Task<Result<EmployeeResponseDTO>> GetEmployeeAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.GetAsync(UrlConstants.Employees.GetOne(id));

        return await response.HandleResponseAsync<EmployeeResponseDTO>(cancellationToken);
    }

    public async Task<Result<EmployeeListResponseDTO>> GetEmployeesAsync(EmployeeGetListRequestDTO request, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.GetAsync($"{UrlConstants.Employees.GET_ALL_URL}?{request.ToQueryString()}");

        return await response.HandleResponseAsync<EmployeeListResponseDTO>(cancellationToken);
    }

    public async Task<Result<EmployeeInsertedResponseDTO>> InsertEmployeeAsync(EmployeeInsertRequestDTO request, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.PostAsJsonAsync(UrlConstants.Employees.InsertUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<EmployeeInsertedResponseDTO>(cancellationToken);
    }

    public async Task<Result<EmployeeUpdatedResponseDTO>> UpdateEmployeeAsync(EmployeeUpdateRequestDTO request, CancellationToken cancellationToken = default)
    {
        await Authorize();

        var response = await _httpClient.PutAsJsonAsync(UrlConstants.Employees.UpdateUrl(), request, cancellationToken);

        return await response.HandleResponseAsync<EmployeeUpdatedResponseDTO>(cancellationToken);
    }

    private async Task Authorize()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await _tokenProvider.GetAccessTokenAsync());
    }
}
