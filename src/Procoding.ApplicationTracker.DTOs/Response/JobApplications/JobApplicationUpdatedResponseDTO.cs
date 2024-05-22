using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.JobApplications;

/// <summary>
/// Response for job application update.
/// </summary>
public sealed class JobApplicationUpdatedResponseDTO
{
    public JobApplicationUpdatedResponseDTO(JobApplicationDTO jobApplicationDto)
    {
        JobApplication = jobApplicationDto;
    }
    public JobApplicationUpdatedResponseDTO()
    {
    }

    /// <summary>
    /// Job application
    /// </summary>
    public JobApplicationDTO JobApplication { get; set; }
}