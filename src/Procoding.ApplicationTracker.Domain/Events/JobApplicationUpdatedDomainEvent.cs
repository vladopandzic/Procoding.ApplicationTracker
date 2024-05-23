using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Events;

public class JobApplicationUpdatedDomainEvent : IDomainEvent
{
    public JobApplicationUpdatedDomainEvent(Guid id, Company company, JobApplicationSource jobApplicationSource, Candidate candidate)
    {
        Id = id;
        Company = company;
        JobApplicationSource = jobApplicationSource;
        Candidate = candidate;
    }

    public Guid Id { get; set; }

    public Company Company { get; set; }

    public JobApplicationSource JobApplicationSource { get; set; }

    public Candidate Candidate { get; set; }
}
