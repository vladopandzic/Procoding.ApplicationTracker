using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
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

    public async Task<Result<CandidateLoginResponseDTO>> LoginCandidate(CandidateLoginRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"{UrlConstants.Candidates.LoginCandidate()}", requestDTO);

        return await response.HandleResponseAsync<CandidateLoginResponseDTO>(cancellationToken);
    }

    public async Task<Result<CandidateSignupResponseDTO>> SignupCandidate(CandidateSignupRequestDTO requestDTO, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsJsonAsync($"{UrlConstants.Candidates.SignupCandidate()}", requestDTO);

        return await response.HandleResponseAsync<CandidateSignupResponseDTO>(cancellationToken);
    }

    public async Task<Result<EmployeeLoginResponseDTO>> RefreshLoginTokenForEmployee(TokenRequestDTO requestDTO, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{UrlConstants.Employees.LoginRefreshEmployee()}", requestDTO);

        return await response.HandleResponseAsync<EmployeeLoginResponseDTO>(cancellationToken);
    }

    public async Task<Result<CandidateLoginResponseDTO>> RefreshLoginTokenForCandidate(TokenRequestDTO requestDTO, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync($"{UrlConstants.Candidates.LoginRefreshCandidate()}", requestDTO);

        return await response.HandleResponseAsync<CandidateLoginResponseDTO>(cancellationToken);
    }
}
