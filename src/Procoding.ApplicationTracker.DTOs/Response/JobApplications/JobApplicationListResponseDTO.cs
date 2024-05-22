using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.JobApplications;

/// <summary>
/// Job application response.
/// </summary>
public sealed class JobApplicationListResponseDTO
{
    public JobApplicationListResponseDTO(IReadOnlyCollection<JobApplicationDTO> jobApplicationDTOs, int totalCount)
    {
        JobApplications = jobApplicationDTOs;
        TotalCount = totalCount;
    }

    public JobApplicationListResponseDTO()
    {
        JobApplications = new List<JobApplicationDTO>();
    }

    /// <summary>
    /// List of job applications
    /// </summary>
    public IReadOnlyCollection<JobApplicationDTO> JobApplications { get; set; }

    public int TotalCount { get; set; }
}