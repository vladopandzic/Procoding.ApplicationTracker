using FluentResults;
using Procoding.ApplicationTracker.DTOs.Response.JobTypes;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface IJobTypeService
{
    /// <summary>
    /// Gets all job types.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Result<JobTypeListResponseDTO>> GetJobTypesAsync(CancellationToken cancellationToken = default);
}
