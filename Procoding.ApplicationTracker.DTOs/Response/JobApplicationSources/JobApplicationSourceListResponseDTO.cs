using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

/// <summary>
/// Job application response.
/// </summary>
public sealed class JobApplicationSourceListResponseDTO
{
    public JobApplicationSourceListResponseDTO(IReadOnlyCollection<JobApplicationSourceDTO> jobApplicationSourceDTOs)
    {
        JobApplicationSources = jobApplicationSourceDTOs;
    }

    public JobApplicationSourceListResponseDTO()
    {
        JobApplicationSources = new List<JobApplicationSourceDTO>();
    }

    /// <summary>
    /// List of job sources
    /// </summary>
    public IReadOnlyCollection<JobApplicationSourceDTO> JobApplicationSources { get; set; }
}
