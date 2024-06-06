using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

internal class CandidateRepository : ICandidateRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<Candidate> _userManager;

    public CandidateRepository(ApplicationDbContext dbContext, UserManager<Candidate> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
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

    public async Task<IdentityResult> InsertAsync(Candidate candidate, string password, CancellationToken cancellationToken)
    {
        return await _userManager.CreateAsync(candidate, password);
    }

    public async Task<bool> ExistsAsync(string email, Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Candidates.AnyAsync(x => x.Email.Value == email && x.Id != id);
    }
}
