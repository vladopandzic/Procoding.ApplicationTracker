namespace Procoding.ApplicationTracker.DTOs.Model;

public class JobApplicationDTO
{
    public JobApplicationDTO(Guid id, CandidateDTO candidate, JobApplicationSourceDTO applicationSource, CompanyDTO company)
    {
        Id = id;
        Candidate = candidate;
        ApplicationSource = applicationSource;
        Company = company;
    }

    public JobApplicationDTO()
    {

    }

    public Guid Id { get; set; }

    public CandidateDTO Candidate { get; set; }

    public JobApplicationSourceDTO ApplicationSource { get; set; }

    public CompanyDTO Company { get; set; }
}
