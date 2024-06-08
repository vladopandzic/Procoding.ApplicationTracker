using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.JobTypes;

public class JobTypeListResponseDTO
{
    public JobTypeListResponseDTO(IReadOnlyCollection<JobTypeDTO> jobTypeDTOs, int totalCount)
    {
        JobTypes = jobTypeDTOs;
        TotalCount = totalCount;
    }

    public JobTypeListResponseDTO()
    {
        JobTypes = new List<JobTypeDTO>();
    }

    /// <summary>
    /// List of job types.
    /// </summary>
    public IReadOnlyCollection<JobTypeDTO> JobTypes { get; set; }

    public int TotalCount { get; set; }
}
