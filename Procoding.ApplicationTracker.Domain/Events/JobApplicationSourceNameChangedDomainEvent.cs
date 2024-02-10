using Procoding.ApplicationTracker.Domain.Events;

/// <summary>
/// Represents event which happens when new job application source name is updated.
/// </summary>
public sealed class JobApplicationSourceUpdatedDomainEvent : IDomainEvent
{
    public JobApplicationSourceUpdatedDomainEvent(Guid id, string newName)
    {
        NewName = newName;
        Id = id;
    }

    public Guid Id { get; }

    public string NewName { get; }
}