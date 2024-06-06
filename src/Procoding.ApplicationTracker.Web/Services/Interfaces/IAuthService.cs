using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface IAuthService
{
    Task<Result<EmployeeLoginResponseDTO>> LoginEmployee(EmployeeLoginRequestDTO requestDTO, CancellationToken cancellationToken = default);

    Task<Result<EmployeeLoginResponseDTO>> RefreshLoginToken(TokenRequestDTO requestDTO, CancellationToken cancellationToken = default);

}
