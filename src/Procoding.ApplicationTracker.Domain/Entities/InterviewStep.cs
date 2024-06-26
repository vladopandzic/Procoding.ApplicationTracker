﻿using Procoding.ApplicationTracker.Domain.Abstractions;
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
    public InterviewStep(JobApplication jobApplication, Guid id, string description, InterviewStepType inteviewStepType) : base(id)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        if (description.Length > MaxLengthForDescription)
        {
            throw new ArgumentException($"Description can not be longer than {MaxLengthForDescription} characters");
        }
        if (jobApplication is null)
        {
            throw new ArgumentNullException($"JobApplication can not be null");

        }
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
    public InterviewStepType InteviewStepType { get; }

    /// <summary>
    /// Job application this inteview step belongs to.
    /// </summary>
    public JobApplication JobApplication { get; }

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; private set; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; private set; }
}
