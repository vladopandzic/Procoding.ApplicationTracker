using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Ardalis.Specification.EntityFrameworkCore;
using Procoding.ApplicationTracker.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Ardalis.Specification;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

internal class CandidateRepository : ICandidateRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CandidateRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Candidate?> GetCandidateAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Candidates.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<Candidate>> GetCandidatesAsync(ISpecification<Candidate> spec, CancellationToken cancellationToken)
    {
        return await _dbContext.Candidates.ToListAsync(spec, cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<Candidate> spec, CancellationToken cancellationToken)
    {
        var query = SpecificationEvaluator.Default.GetQuery(_dbContext.Candidates, spec, true);
        return await query.CountAsync();
    }

    public async Task InsertAsync(Candidate candidate, CancellationToken cancellationToken)
    {
        await _dbContext.Candidates.AddAsync(candidate, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string email, Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Candidates.AnyAsync(x => x.Email.Value == email && x.Id != id);
    }
}
