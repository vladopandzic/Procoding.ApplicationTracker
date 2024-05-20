using Procoding.ApplicationTracker.DTOs.Request.Companies;
using Procoding.ApplicationTracker.DTOs.Response.Companies;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface ICompanyService
{
    /// <summary>
    /// Gets company by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CompanyResponseDTO?> GetCompanyAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all companies.
    /// </summary>
    /// <returns></returns>
    Task<CompanyListResponseDTO?> GetCompaniesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts one company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CompanyInsertedResponseDTO?> InsertCompanyAsync(CompanyInsertRequestDTO request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates one company.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CompanyUpdatedResponseDTO?> UpdateCompanyAsync(CompanyUpdateRequestDTO request, CancellationToken cancellationToken = default);
}
