using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Events;

public sealed class JobApplicationUpdatedDomainEvent : IDomainEvent
{
    public JobApplicationUpdatedDomainEvent(Guid id, Company company, JobApplicationSource jobApplicationSource, Candidate candidate)
    {
        Id = id;
        Company = company;
        JobApplicationSource = jobApplicationSource;
        Candidate = candidate;
    }

    public Guid Id { get; }

    public Company Company { get; }

    public JobApplicationSource JobApplicationSource { get; }

    public Candidate Candidate { get; }
}
