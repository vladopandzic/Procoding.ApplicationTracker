using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

internal sealed class JobApplicationSourceRepository : IJobApplicationSourceRepository
{
    private readonly ApplicationDbContext _dbContext;

    public JobApplicationSourceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<JobApplicationSource>> GetJobApplicationSourceAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.JobApplicationSources.ToListAsync(cancellationToken);
    }

    public async Task<JobApplicationSource?> GetJobApplicationSourceAsync(Guid jobApplicationSourceId, CancellationToken cancellationToken)
    {
        return await _dbContext.JobApplicationSources.FirstOrDefaultAsync(x => x.Id == jobApplicationSourceId, cancellationToken);
    }

    public async Task InsertAsync(JobApplicationSource jobApplicationSource, CancellationToken cancellationToken)
    {
        await _dbContext.JobApplicationSources.AddAsync(jobApplicationSource, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string name, Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.JobApplicationSources.AnyAsync(x => x.Name == name && x.Id != id);
    }
}
