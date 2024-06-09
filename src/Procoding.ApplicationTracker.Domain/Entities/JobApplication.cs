using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Events;
using Procoding.ApplicationTracker.Domain.ValueObjects;

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
                           JobApplicationStatus jobApplicationStatus,
                           JobType jobType,
                           WorkLocationType workLocationType,
                           string jobPositionTitle,
                           Link jobAdLink,
                           string description) : base(id)
    {
        Candidate = candidate;
        AppliedOnUTC = appliedOnUTC;
        ApplicationSource = applicationSource;
        Company = company;
        JobApplicationStatus = jobApplicationStatus;
        JobType = jobType;
        WorkLocationType = workLocationType;
        JobPositionTitle = jobPositionTitle;
        Description = description;
        JobAdLink = jobAdLink;
    }

    /// <summary>
    /// Creates new job application.
    /// </summary>
    /// <param name="candidate"></param>
    /// <param name="id"></param>
    /// <param name="jobApplicationSource"></param>
    /// <param name="company"></param>
    /// <param name="timeProvider"></param>
    /// <returns></returns>
    public static JobApplication Create(Candidate candidate,
                                        Guid id,
                                        JobApplicationSource jobApplicationSource,
                                        Company company,
                                        string jobPositionTitle,
                                        Link jobAdLink,
                                        JobType jobType,
                                        WorkLocationType workLocationType,
                                        string? description,
                                        TimeProvider timeProvider)
    {
        var newJobApplication = new JobApplication(candidate: candidate,
                                                   id: id,
                                                   appliedOnUTC: timeProvider.GetUtcNow().DateTime,
                                                   applicationSource: jobApplicationSource,
                                                   company: company,
                                                   jobPositionTitle: jobPositionTitle,
                                                   jobAdLink: jobAdLink,
                                                   jobType: jobType,
                                                   workLocationType: workLocationType,
                                                   description: description,
                                                   jobApplicationStatus: JobApplicationStatus.Applied);

        newJobApplication.AddDomainEvent(new AppliedForAJobDomainEvent(newJobApplication));

        return newJobApplication;
    }

    /// <summary>
    /// Creates new interview for job application.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="description"></param>
    /// <param name="inteviewStepType"></param>
    /// <returns></returns>
    public InterviewStep CreateNewInterview(Guid id, string description, InterviewStepType inteviewStepType)
    {
        var interview = new InterviewStep(jobApplication: this, id: id, description: description, inteviewStepType: inteviewStepType);

        _interviewSteps.Add(interview);

        this.AddDomainEvent(new NewInterviewAddedDomainEvent(interview));

        return interview;
    }

    public JobApplication Update(Company company,
                                 JobApplicationSource jobApplicationSource,
                                 Candidate candidate,
                                 string jobPositionTitle,
                                 WorkLocationType workLocationType,
                                 JobType jobType,
                                 Link jobAdLink,
                                 string? description)
    {
        Company = company;
        ApplicationSource = jobApplicationSource;
        Candidate = candidate;
        JobPositionTitle = jobPositionTitle;
        JobType = jobType;
        WorkLocationType = workLocationType;
        JobAdLink = jobAdLink;
        Description = description;
        this.AddDomainEvent(new JobApplicationUpdatedDomainEvent(id: Id,
                                                                 company: company,
                                                                 jobApplicationSource: jobApplicationSource,
                                                                 candidate: candidate,
                                                                 jobPositionTitle: jobPositionTitle,
                                                                 workLocationType: workLocationType,
                                                                 jobType: jobType,
                                                                 jobAdLink: jobAdLink,
                                                                 description: description));

        return this;
    }

    /// <summary>
    /// Represents time when candidate applied for the job.
    /// </summary>
    public DateTime AppliedOnUTC { get; }

    /// <summary>
    /// Application source through candidate applied.
    /// </summary>
    public JobApplicationSource ApplicationSource { get; private set; }

    /// <summary>
    /// Current job application status.
    /// </summary>
    public JobApplicationStatus JobApplicationStatus { get; }

    /// <summary>
    /// Job type like <see cref="JobType.FullTime"/> or <see cref="JobType.PartTime"/>.
    /// </summary>
    public JobType JobType { get; private set; }

    /// <summary>
    /// Work location type like <see cref="WorkLocationType.Remote"/> or <see cref="WorkLocationType.Hybrid"/>.
    /// </summary>
    public WorkLocationType WorkLocationType { get; private set; }

    //TODO: make it as separate table?
    /// <summary>
    /// Title for the position like Senior .NET software engineer.
    /// </summary>
    public string JobPositionTitle { get; private set; }

    public string? Description { get; private set; }

    public Link JobAdLink { get; private set; }

    /// <summary>
    /// Company the candidate applies for.
    /// </summary>
    public Company Company { get; private set; }

    /// <summary>
    /// The candidate for the job.
    /// </summary>
    public Candidate Candidate { get; private set; }

    /// <summary>
    /// Interview steps each job application has. Using AsReadOnly() will create a read only wrapper around the private
    /// list so is protected against "external updates". It's much cheaper than .ToList() because it will not have to
    /// copy all items in a new collection. (Just one heap alloc for the wrapper instance)
    /// https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx
    /// </summary>
    public IReadOnlyList<InterviewStep> InterviewSteps => _interviewSteps.AsReadOnly();

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; private set; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; private set; }
}
