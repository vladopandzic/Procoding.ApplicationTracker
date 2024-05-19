using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Repositories;

/// <summary>
/// Job application source interface
/// </summary>
public interface IJobApplicationSourceRepository
{
    /// <summary>
    /// Inserts the specified jobApplicationSource to the database.
    /// </summary>
    /// <param name="jobApplicationSource">The jobApplicationSource to be inserted to the database.</param>
    Task InsertAsync(JobApplicationSource jobApplicationSource, CancellationToken cancellationToken);

    /// <summary>
    /// Gets list of job application source.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<JobApplicationSource>> GetJobApplicationSourceAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Get one job application source.
    /// </summary>
    /// <param name="jobApplicationSourceId"></param>
    /// <returns></returns>
    Task<JobApplicationSource?> GetJobApplicationSourceAsync(Guid jobApplicationSourceId, CancellationToken cancellationToken);
}
