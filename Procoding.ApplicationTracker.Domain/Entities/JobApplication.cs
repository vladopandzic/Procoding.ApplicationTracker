using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Events;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents job application.
/// </summary>
public sealed class JobApplication : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{

    private readonly List<InterviewStep> _interviewSteps = new();

    /// <summary>
    /// Creates new instance of <see cref="JobApplication"/>. Required by EF Core.
    /// </summary>
#pragma warning disable CS8618
    private JobApplication()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of <see cref="JobApplication"/>.
    /// </summary>
    /// <param name="candidate">Candidate for this job application.</param>
    /// <param name="id">Id of the job application.</param>
    /// <param name="appliedOnUTC">When the candidate applied.</param>
    /// <param name="applicationSource">Source through candidate applied like LinkedIn, etc.</param>
    /// <param name="company">Company that the candidate applies for.</param>
    /// <param name="jobApplicationStatus">Job application status.</param>
    private JobApplication(Candidate candidate,
                          Guid id,
                          DateTime appliedOnUTC,
                          JobApplicationSource applicationSource,
                          Company company,
                          JobApplicationStatus jobApplicationStatus) : base(id)
    {
        Candidate = candidate;
        AppliedOnUTC = appliedOnUTC;
        ApplicationSource = applicationSource;
        Company = company;
        JobApplicationStatus = jobApplicationStatus;
    }

    public JobApplication ApplyForAJob(Candidate candidate,
                                       Guid id,
                                       JobApplicationSource jobApplicationSource,
                                       Company company,
                                      TimeProvider timeProvider)
    {

        var newJobApplication = new JobApplication(candidate: candidate,
                                                   id: id,
                                                   appliedOnUTC: timeProvider.GetUtcNow().DateTime,
                                                   applicationSource: jobApplicationSource,
                                                   company: company,
                                                   jobApplicationStatus: JobApplicationStatus.Applied);

        this.AddDomainEvent(new AppliedForAJobDomainEvent(newJobApplication));

        return newJobApplication;

    }

    public void NewInterview(Guid id, string description, InteviewStepType inteviewStepType)
    {
        var interview = new InterviewStep(jobApplication: this,
                                          id: id,
                                          description: description,
                                          inteviewStepType: inteviewStepType);

        this.AddDomainEvent(new NewInterviewAddedDomainEvent(interview));
    }

    /// <summary>
    /// Represents time when candidate applied for the job.
    /// </summary>
    public DateTime AppliedOnUTC { get; }

    /// <summary>
    /// Application source through candidate applied.
    /// </summary>
    public JobApplicationSource ApplicationSource { get; }

    /// <summary>
    /// Current job application status.
    /// </summary>
    public JobApplicationStatus JobApplicationStatus { get; }

    /// <summary>
    /// Company the candidate applies for.
    /// </summary>
    public Company Company { get; }

    /// <summary>
    /// The candidate for the job.
    /// </summary>
    public Candidate Candidate { get; }

    /// <summary>
    /// Interview steps each job application has.
    /// </summary>
    public IReadOnlyList<InterviewStep> InterviewSteps => _interviewSteps.ToList();
    
    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }
}
