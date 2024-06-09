using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Events;

public sealed class JobApplicationUpdatedDomainEvent : IDomainEvent
{
    public JobApplicationUpdatedDomainEvent(Guid id,
                                            Company company,
                                            JobApplicationSource jobApplicationSource,
                                            Candidate candidate,
                                            string jobPositionTitle,
                                            WorkLocationType workLocationType,
                                            JobType jobType,
                                            Link jobAdLink,
                                            string? description)
    {
        Id = id;
        Company = company;
        JobApplicationSource = jobApplicationSource;
        Candidate = candidate;
        Description = description;
        JobPositionTitle = jobPositionTitle;
        WorkLocationType = workLocationType;
        JobType = jobType;
        JbbAdLink = jobAdLink;
    }

    public Guid Id { get; }

    public Company Company { get; }

    public JobApplicationSource JobApplicationSource { get; }

    public Candidate Candidate { get; }

    public string? Description { get; }

    public string JobPositionTitle { get; }

    public WorkLocationType WorkLocationType { get; }

    public JobType JobType { get; }

    public Link JbbAdLink { get; }

}
