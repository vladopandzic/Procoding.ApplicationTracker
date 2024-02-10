using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Events;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents job application source like Linkedin.
/// </summary>
public sealed class JobApplicationSource : AggregateRoot
{
    /// <summary>
    /// Max length name can have.
    /// </summary>
    public static readonly int MaxLengthForName = 255;

    /// <summary>
    /// Min length name can have.
    /// </summary>
    public static readonly int MinLengthForName = 3;

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
    private JobApplicationSource(Guid id, string name) : base(id)
    {
        ValidateName(name);
        Name = name;
    }

    private static void ValidateName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.Length > MaxLengthForName)
        {
            throw new ArgumentException($"Name can not be longer than {MaxLengthForName} characters");
        }
        if (name.Length < MinLengthForName)
        {
            throw new ArgumentException($"Name can not be shorter than {MinLengthForName} characters");
        }
    }

    /// <summary>
    /// Represents the name of the job source.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Creates new <see cref="JobApplicationSource"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public static JobApplicationSource Create(Guid id, string name)
    {
        var jobApplicationSource = new JobApplicationSource(id, name);
        jobApplicationSource.AddDomainEvent(new JobApplicationSourceCreatedDomainEvent(jobApplicationSource));
        return jobApplicationSource;
    }

    /// <summary>
    /// Updates the name of the <see cref="JobApplicationSource"/>.
    /// </summary>
    /// <param name="name"></param>
    public void ChangeName(string name)
    {
        ValidateName(name);

        if (string.Equals(Name, name, StringComparison.InvariantCulture))
        {
            return;
        }

        Name = name;

        AddDomainEvent(new JobApplicationSourceUpdatedDomainEvent(Id, name));
    }
}
