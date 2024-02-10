using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Events;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Procoding.ApplicationTracker.Domain.Entities;

/// <summary>
/// Represents job interview candidate.
/// </summary>
public sealed class Candidate : AggregateRoot, ISoftDeletableEntity, IAuditableEntity
{

    private readonly List<JobApplication> _jobApplications = new();

    /// <summary>
    /// Max allowed length for the <see cref="Name"/> property.
    /// </summary>
    public static readonly int MaxLengthForName = 512;

    /// <summary>
    /// Max allowed length for the <see cref="Surname"/> property.
    /// </summary>
    public static readonly int MaxLengthForSurname = 512;


    /// <summary>
    /// Max allowed length for the <see cref="Name"/> property.
    /// </summary>
    public static readonly int MinLengthForName = 2;

    /// <summary>
    /// Max allowed length for the <see cref="Surname"/> property.
    /// </summary>
    public static readonly int MinLengthForSurname = 2;


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
    /// Name of the candidate. Must be at least 2 characters long and not longer than <see cref="MaxLengthForName"/>
    /// characters.
    /// </param>
    /// <param name="surname">
    /// Surname of the candidate. Must be at least 2 characters long and not longer than <see
    /// cref="MaxLengthForSurname"/> characters.
    /// </param>
    /// <param name="email">Email for the candidate. Must be valid email address.</param>
    /// <exception cref="ArgumentException"></exception>
    private Candidate(Guid id, string name, string surname, Email email) : base(id)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(surname);

        if (name.Length > MaxLengthForName)
        {
            throw new ArgumentException($"Name can not be longer than {MaxLengthForName} characters");
        }
        if (surname.Length > MaxLengthForSurname)
        {
            throw new ArgumentException($"Surname can not be longer than {MaxLengthForSurname} characters");
        }

        if (name.Length < MinLengthForName)
        {
            throw new ArgumentException($"Name can not be shorter than {MinLengthForName} characters");
        }
        if (surname.Length > MaxLengthForSurname)
        {
            throw new ArgumentException($"Surname can not be longer than {MinLengthForName} characters");
        }
        Name = name;
        Surname = surname;
        Email = email;
    }

    public Candidate Create(Guid id, string name, string surname, Email email)
    {
        var candidate = new Candidate(id: id,
                                      name: name,
                                      surname: surname,
                                      email: email);

        this.AddDomainEvent(new CandidateCreatedDomainEvent(candidate));

        return candidate;
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
    public IReadOnlyList<JobApplication> JobApplications => _jobApplications.ToList();

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }
}
