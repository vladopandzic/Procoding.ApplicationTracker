using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using Procoding.ApplicationTracker.Web.Extensions;
using Procoding.ApplicationTracker.Web.Services.Interfaces;

namespace Procoding.ApplicationTracker.Web.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Result<EmployeeLoginResponseDTO>> LoginEmployee(EmployeeLoginRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"{UrlConstants.Employees.LoginEmployee()}", requestDTO);

        return await response.HandleResponseAsync<EmployeeLoginResponseDTO>(cancellationToken);
    }

    public async Task<Result<EmployeeLoginResponseDTO>> RefreshLoginToken(TokenRequestDTO requestDTO, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{UrlConstants.Employees.LoginRefreshEmployee()}", requestDTO);

        return await response.HandleResponseAsync<EmployeeLoginResponseDTO>(cancellationToken);
    }
}
