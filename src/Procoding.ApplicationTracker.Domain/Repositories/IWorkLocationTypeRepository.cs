using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Repositories;

public interface IWorkLocationTypeRepository
{
    /// <summary>
    /// Gets all job types.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<WorkLocationType>> GetAllWorkLocationTypesAsync(CancellationToken cancellationToken);
}
