using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

public class WorkLocationType : ValueObject
{
    public static readonly WorkLocationType Remote = new WorkLocationType(nameof(Remote));
    public static readonly WorkLocationType OnSite = new WorkLocationType(nameof(OnSite));
    public static readonly WorkLocationType Hybrid = new WorkLocationType(nameof(Hybrid));


    /// <summary>
    /// Gets all work location types.
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<WorkLocationType> GetAll()
    {
        return new[] { Remote, OnSite, Hybrid };
    }

    public WorkLocationType(string value)
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
