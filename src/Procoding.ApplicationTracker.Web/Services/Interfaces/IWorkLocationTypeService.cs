using FluentResults;
using Procoding.ApplicationTracker.DTOs.Model;
using Procoding.ApplicationTracker.DTOs.Response;
using Procoding.ApplicationTracker.DTOs.Response.WorkLocationTypes;

namespace Procoding.ApplicationTracker.Web.Services.Interfaces;

public interface IWorkLocationTypeService
{
    /// <summary>
    /// Gets all work location types.
    /// </summary>
    /// <returns></returns>
    Task<Result<WorkLocationTypeListResponse>> GetWorkLocationTypesAsync(CancellationToken cancellationToken = default);
}
