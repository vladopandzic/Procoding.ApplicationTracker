using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents job application.
/// </summary>
public class JobApplication : EntityBase, ISoftDeletableEntity, IAuditableEntity
{
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
    public JobApplication(Candidate candidate,
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
        InterviewSteps = new List<InterviewStep>();
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
    public ICollection<InterviewStep> InterviewSteps { get; }

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }
}
