using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

internal class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CompanyRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Company>> GetCompaniesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Companies.ToListAsync(cancellationToken);
    }

    public async Task<Company?> GetCompanyAsync(Guid companyId, CancellationToken cancellationToken)
    {
        return await _dbContext.Companies.FirstOrDefaultAsync(x => x.Id == companyId, cancellationToken);
    }

    public async Task InsertAsync(Company company, CancellationToken cancellationToken)
    {
        await _dbContext.Companies.AddAsync(company, cancellationToken);
    }
}
