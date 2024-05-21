using FluentResults;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface IJobApplicationSourceService
{
    /// <summary>
    /// Gets job application source by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<JobApplicationSourceResponseDTO>> GetJobApplicationSourceAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all job application sources
    /// </summary>
    /// <returns></returns>
    Task<Result<JobApplicationSourceListResponseDTO>> GetJobApplicationSourcesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Inserts one job application source
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<JobApplicationSourceInsertedResponseDTO>> InsertJobApplicationSourceAsync(JobApplicationSourceInsertRequestDTO request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates one job application source
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<JobApplicationSourceUpdatedResponseDTO>> UpdateJobApplicationSourceAsync(JobApplicationSourceUpdateRequestDTO request, CancellationToken cancellationToken = default);
}
