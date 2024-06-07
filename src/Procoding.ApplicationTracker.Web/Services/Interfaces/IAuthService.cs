using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface IAuthService
{
    Task<Result<CandidateLoginResponseDTO>> RefreshLoginTokenForCandidate(TokenRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task<Result<CandidateSignupResponseDTO>> SignupCandidate(CandidateSignupRequestDTO requestDTO, CancellationToken cancellationToken);
    Task<Result<CandidateLoginResponseDTO>> LoginCandidate(CandidateLoginRequestDTO requestDTO, CancellationToken cancellationToken);
    Task<Result<EmployeeLoginResponseDTO>> LoginEmployee(EmployeeLoginRequestDTO requestDTO, CancellationToken cancellationToken = default);

    Task<Result<EmployeeLoginResponseDTO>> RefreshLoginTokenForEmployee(TokenRequestDTO requestDTO, CancellationToken cancellationToken = default);

}
