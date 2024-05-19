using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Events;

/// <summary>
/// Represents event which happens when new job application source is added.
/// </summary>
public sealed class JobApplicationSourceCreatedDomainEvent : IDomainEvent
{
    public JobApplicationSourceCreatedDomainEvent(JobApplicationSource jobApplicationSource)
    {
        JobApplicationSource = jobApplicationSource;
    }

    public JobApplicationSource JobApplicationSource { get; }
}