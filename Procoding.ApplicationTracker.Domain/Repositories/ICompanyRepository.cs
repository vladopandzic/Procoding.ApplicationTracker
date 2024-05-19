using Procoding.ApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Repositories;

public interface ICompanyRepository
{
    /// <summary>
    /// Inserts the specified company to the database.
    /// </summary>
    /// <param name="company">The company to be inserted to the database.</param>
    Task InsertAsync(Company company, CancellationToken cancellationToken);

    /// <summary>
    /// Gets list of companies.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<Company>> GetCompaniesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get one company.
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    Task<Company?> GetCompanyAsync(Guid companyId, CancellationToken cancellationToken);
}
