namespace Procoding.ApplicationTracker.DTOs.Model;

public class JobApplicationDTO
{
    public JobApplicationDTO(Guid id, CandidateDTO candidate, JobApplicationSourceDTO jobApplicationSource, CompanyDTO company)
    {
        Id = id;
        Candidate = candidate;
        JobApplicationSource = jobApplicationSource;
        Company = company;
    }

    public JobApplicationDTO()
    {

    }

    public Guid Id { get; set; }

    public CandidateDTO Candidate { get; set; }

    public JobApplicationSourceDTO JobApplicationSource { get; set; }

    public CompanyDTO Company { get; set; }
}
