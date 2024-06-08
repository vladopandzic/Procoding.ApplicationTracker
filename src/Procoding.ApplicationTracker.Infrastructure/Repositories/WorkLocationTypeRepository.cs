using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Infrastructure.Repositories;

internal class WorkLocationTypeRepository : IWorkLocationTypeRepository
{
    public Task<List<WorkLocationType>> GetAllWorkLocationTypesAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(WorkLocationType.GetAll().ToList());
    }
}
