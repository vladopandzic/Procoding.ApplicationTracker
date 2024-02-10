using Procoding.ApplicationTracker.Domain.Entities;

namespace Procoding.ApplicationTracker.Domain.Events;
/// <summary>
/// Represents event which happens when new interview step is added.
/// </summary>
public class NewInterviewAddedDomainEvent : IDomainEvent
{
    public NewInterviewAddedDomainEvent(InterviewStep interviewStep)
    {
        InterviewStep = interviewStep;
    }

    public InterviewStep InterviewStep { get; }
}