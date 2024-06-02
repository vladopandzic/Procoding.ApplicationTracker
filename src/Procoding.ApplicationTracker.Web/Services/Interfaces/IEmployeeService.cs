using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface IEmployeeService
{
    /// <summary>
    /// Gets candidate by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<EmployeeResponseDTO>> GetEmployeeAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all candidates.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Result<EmployeeListResponseDTO>> GetEmployeesAsync(EmployeeGetListRequestDTO request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts one candidate.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<EmployeeInsertedResponseDTO>> InsertEmployeeAsync(EmployeeInsertRequestDTO request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates one candidate.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<EmployeeUpdatedResponseDTO>> UpdateEmployeeAsync(EmployeeUpdateRequestDTO request, CancellationToken cancellationToken = default);
}