namespace Procoding.ApplicationTracker.Domain.Entities;

public class InterviewProcess
{
    public List<InterviewStep> InterviewSteps { get; }

    public InterviewProcess(List<InterviewStep> interviewSteps)
    {
        InterviewSteps = interviewSteps;
    }
}
