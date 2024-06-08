using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.WorkLocationTypes;

public class WorkLocationTypeListResponse
{
    public WorkLocationTypeListResponse(IReadOnlyCollection<WorkLocationTypeDTO> workLocationTypeDTOs, int totalCount)
    {
        WorkLocationTypes = workLocationTypeDTOs;
        TotalCount = totalCount;
    }

    public WorkLocationTypeListResponse()
    {
        WorkLocationTypes = new List<WorkLocationTypeDTO>();
    }

    /// <summary>
    /// List of job types.
    /// </summary>
    public IReadOnlyCollection<WorkLocationTypeDTO> WorkLocationTypes { get; set; }

    public int TotalCount { get; set; }
}
