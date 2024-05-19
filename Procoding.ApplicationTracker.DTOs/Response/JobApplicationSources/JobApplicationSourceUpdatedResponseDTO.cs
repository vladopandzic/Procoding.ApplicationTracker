using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;

public class JobApplicationSourceUpdatedResponseDTO
{
    public JobApplicationSourceUpdatedResponseDTO(JobApplicationSourceDTO jobApplicationSourceDto)
    {
        JobApplicationSource = jobApplicationSourceDto;
    }
    public JobApplicationSourceUpdatedResponseDTO()
    {
    }

    public JobApplicationSourceDTO JobApplicationSource { get; set; }
}