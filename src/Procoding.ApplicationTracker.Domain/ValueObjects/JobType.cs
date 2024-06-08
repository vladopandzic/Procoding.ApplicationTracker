using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

public class JobType : ValueObject
{
    public static readonly JobType FullTime = new JobType(nameof(FullTime));
    public static readonly JobType PartTime = new JobType(nameof(PartTime));
    public static readonly JobType Contract = new JobType(nameof(Contract));
    public static readonly JobType Temporary = new JobType(nameof(Temporary));
    public static readonly JobType Volunteer = new JobType(nameof(Volunteer));

    /// <summary>
    /// Gets all job types.
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<JobType> GetAll()
    {
        return new[] { FullTime, PartTime, Contract, Temporary, Volunteer };
    }

    public JobType(string value)
    {
        Value = value;
    }

    /// <summary>
    /// Actual value for the job type.
    /// </summary>
    public string Value { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return [Value];
    }
}
