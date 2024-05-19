using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Response;

public class JobApplicationSourceResponseDTO
{
    public JobApplicationSourceResponseDTO(JobApplicationSourceDTO jobApplicationSourceDto)
    {
        JobApplicationSource = jobApplicationSourceDto;
    }
    public JobApplicationSourceResponseDTO()
    {
            
    }

    public JobApplicationSourceDTO JobApplicationSource { get; set; }
}
