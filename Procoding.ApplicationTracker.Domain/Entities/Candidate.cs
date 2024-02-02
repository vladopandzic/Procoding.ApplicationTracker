using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents job interview candidate.
/// </summary>
public sealed class Candidate : EntityBase, ISoftDeletableEntity, IAuditableEntity
{
    /// <summary>
    /// Max allowed length for the <see cref="Name"/> property.
    /// </summary>
    public static readonly int MaxLengthForName = 512;

    /// <summary>
    /// Max allowed length for the <see cref="Surname"/> property.
    /// </summary>
    public static readonly int MaxLengthForSurname = 512;


    /// <summary>
    /// Initializies new instance of <see cref="Candidate"/>. Required only by EF Core.
    /// </summary>
#pragma warning disable CS8618
    private Candidate()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of <see cref="Candidate"/>.
    /// </summary>
    /// <param name="id">Id of the candidate. Must be unique.</param>
    /// <param name="name">
    /// Name of the candidate. Must be at least 3 characters long and not longer than <see cref="MaxLengthForName"/>
    /// characters.
    /// </param>
    /// <param name="surname">
    /// Surname of the candidate. Must be at least 3 characters long and not longer than <see
    /// cref="MaxLengthForSurname"/> characters.
    /// </param>
    /// <param name="email">Email for the candidate. Must be valid email address.</param>
    /// <exception cref="ArgumentException"></exception>
    public Candidate(Guid id, string name, string surname, Email email) : base(id)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(surname);

        if(name.Length > MaxLengthForName)
        {
            throw new ArgumentException($"Name can not be longer than {name.Length} characters");
        }
        if(surname.Length > MaxLengthForSurname)
        {
            throw new ArgumentException($"Surname can not be longer than {name.Length} characters");
        }
        Name = name;
        Surname = surname;
        Email = email;
        JobApplications = new List<JobApplication>();
    }

    /// <summary>
    /// Candidates name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Candidates surname
    /// </summary>
    public string Surname { get; }

    /// <summary>
    /// Candidates email address.
    /// </summary>
    public Email Email { get; }

    /// <summary>
    /// Job applications for the candidate.
    /// </summary>
    public ICollection<JobApplication> JobApplications { get; }

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }
}
