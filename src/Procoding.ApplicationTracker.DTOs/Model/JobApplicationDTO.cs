namespace Procoding.ApplicationTracker.DTOs.Model;

public class JobApplicationDTO
{
    public JobApplicationDTO()
    {
    }

    public JobApplicationDTO(Guid id,
                             CandidateDTO candidate,
                             JobApplicationSourceDTO applicationSource,
                             CompanyDTO company,
                             string jobPositionTitle,
                             string jobAdLink,
                             WorkLocationTypeDTO workLocation,
                             JobTypeDTO jobType,
                             string? description)
    {
        Id = id;
        Candidate = candidate;
        ApplicationSource = applicationSource;
        Company = company;
        JobPositionTitle = jobPositionTitle;
        JobAdLink = jobAdLink;
        WorkLocation = workLocation;
        JobType = jobType;
        Description = description;
    }

    public Guid Id { get; set; }

    public CandidateDTO Candidate { get; set; }

    public JobApplicationSourceDTO ApplicationSource { get; set; }

    public CompanyDTO Company { get; set; }

    public string JobPositionTitle { get; set; }

    public string JobAdLink { get; set; }

    public WorkLocationTypeDTO WorkLocation { get; set; }

    public JobTypeDTO JobType { get; set; }

    public string? Description { get; set; }
}
