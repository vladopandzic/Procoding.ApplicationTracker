using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.JobApplications;


/// <summary>
/// Response for job application inserted.
/// </summary>
public sealed class JobApplicationInsertedResponseDTO
{
    public JobApplicationInsertedResponseDTO(JobApplicationDTO jobApplicationDTO)
    {
        JobApplication = jobApplicationDTO;
    }

    public JobApplicationInsertedResponseDTO()
    {
    }

    /// <summary>
    /// Job application.
    /// </summary>
    public JobApplicationDTO JobApplication { get; set; }
}