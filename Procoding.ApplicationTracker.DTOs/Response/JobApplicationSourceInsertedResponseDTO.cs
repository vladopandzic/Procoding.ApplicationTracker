using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response;

public class JobApplicationSourceInsertedResponseDTO
{
    public JobApplicationSourceInsertedResponseDTO(JobApplicationSourceDTO jobApplicationSourceDto)
    {
        JobApplicationSource = jobApplicationSourceDto;
    }
    public JobApplicationSourceInsertedResponseDTO()
    {
    }

    public JobApplicationSourceDTO JobApplicationSource { get; set; }
}