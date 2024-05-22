using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.JobApplications;

/// <summary>
/// Response for getting one job application.
/// </summary>
public sealed class JobApplicationResponseDTO
{
    public JobApplicationResponseDTO(JobApplicationDTO jobApplicationDto)
    {
        JobApplication = jobApplicationDto;
    }

    public JobApplicationResponseDTO()
    {

    }

    /// <summary>
    /// Job application.
    /// </summary>
    public JobApplicationDTO JobApplication { get; set; }
}
