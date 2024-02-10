using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using System.Xml.Linq;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents one step in job application interview process.
/// </summary>
public sealed class InterviewStep : EntityBase, IAuditableEntity, ISoftDeletableEntity
{
    public static readonly int MaxLengthForDescription = 255;

#pragma warning disable CS8618
    private InterviewStep()
    {
    } //used by EF core
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of interview step.
    /// </summary>
    /// <param name="jobApplication">Job application.</param>
    /// <param name="id">Id of the interview step.</param>
    /// <param name="description">Description of the interview step.</param>
    /// <param name="inteviewStepType">Intervies step type.</param>
    public InterviewStep(JobApplication jobApplication, Guid id, string description, InteviewStepType inteviewStepType) : base(id)
    {
        if (description.Length > MaxLengthForDescription)
        {
            throw new ArgumentException($"Description can not be longer than {MaxLengthForDescription} characters");
        }
        ArgumentException.ThrowIfNullOrEmpty(description);
        Description = description;
        JobApplication = jobApplication;
        InteviewStepType = inteviewStepType;
    }

    /// <summary>
    /// Description of the interview step.
    /// </summary>
    public string Description { get; }

    /// <summary>
    /// Interview step type.
    /// </summary>
    public InteviewStepType InteviewStepType { get; }

    /// <summary>
    /// Job application this inteview step belongs to.
    /// </summary>
    public JobApplication JobApplication { get; }

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }
}
