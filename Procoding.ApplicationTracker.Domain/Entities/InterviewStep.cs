namespace Procoding.ApplicationTracker.Domain.Entities;

public enum InteviewStepType
{
    Initial,
    Technical,
    None
}

public sealed class InterviewStep
{
    public static InterviewStep None { get; } = new InterviewStep(string.Empty, InteviewStepType.None);

    public InterviewStep(string name, InteviewStepType inteviewStepType)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;
        InteviewStepType = inteviewStepType;
    }

    public string Name { get; }

    public InteviewStepType InteviewStepType { get; }
}
