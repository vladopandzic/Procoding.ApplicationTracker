using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents job application source like Linkedin.
/// </summary>
public sealed class JobApplicationSource : EntityBase
{
    /// <summary>
    /// Max length name can have.
    /// </summary>
    public static readonly int MaxLengthForName = 255;

#pragma warning disable CS8618
    private JobApplicationSource()
    {
    } //used by EF core
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of the <see cref="JobApplicationSource"/>.
    /// </summary>
    /// <param name="id">Id of the job application source.</param>
    /// <param name="name">Name of the job application source.</param>
    /// <exception cref="ArgumentException"></exception>
    public JobApplicationSource(Guid id, string name) : base(id)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if(name.Length > MaxLengthForName)
        {
            throw new ArgumentException($"Name can not be longer than {MaxLengthForName} characters");
        }
        Name = name;
    }

    /// <summary>
    /// Represents the name of the job source.
    /// </summary>
    public string Name { get; }
}
