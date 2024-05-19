using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Infrastructure.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

    public async Task<List<Candidate>> GetCandidatesAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Candidates.ToListAsync();
    }

    public async Task InsertAsync(Candidate candidate, CancellationToken cancellationToken)
    {
        await _dbContext.Candidates.AddAsync(candidate, cancellationToken);
    }
}
