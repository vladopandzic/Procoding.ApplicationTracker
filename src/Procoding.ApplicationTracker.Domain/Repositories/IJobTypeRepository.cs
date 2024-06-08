using Procoding.ApplicationTracker.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Repositories;

public interface IJobTypeRepository
{
    /// <summary>
    /// Gets all job types.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<JobType>> GetAllJobTypesAsync(CancellationToken cancellationToken);
}
