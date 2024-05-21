using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface ICompanyService
{
    /// <summary>
    /// Gets company by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<CompanyResponseDTO>> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all companies.
    /// </summary>
    /// <returns></returns>
    Task<Result<CompanyListResponseDTO>> GetCompaniesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts one company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<CompanyInsertedResponseDTO>> InsertCompanyAsync(CompanyInsertRequestDTO request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates one company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<CompanyUpdatedResponseDTO>> UpdateCompanyAsync(CompanyUpdateRequestDTO request, CancellationToken cancellationToken = default);
}
