using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

internal class JobTypeRepository : IJobTypeRepository
{
    public Task<List<JobType>> GetAllJobTypesAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(JobType.GetAll().ToList());

    }
}
