using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Entities;

public class Employee : IdentityUser<Guid>
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
    /// Max allowed length for the <see cref="Name"/> property.
    /// </summary>
    public static readonly int MinLengthForName = 2;

    /// <summary>
    /// Max allowed length for the <see cref="Surname"/> property.
    /// </summary>
    public static readonly int MinLengthForSurname = 2;

    /// <summary>
    /// Candidates name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Candidates surname
    /// </summary>
    public string Surname { get; private set; }

    /// <summary>
    /// Candidates email address.
    /// </summary>
    public new Email Email { get; private set; }


    /// <summary>
    /// Initializies new instance of <see cref="Candidate"/>. Required only by EF Core.
    /// </summary>
#pragma warning disable CS8618
    public Employee()
    {
    }

#pragma warning restore CS8618
    /// <summaryEmployee
    /// Creates new instance of <see cref="Employee"/>.
    /// </summary>
    /// <param name="id">Id of the Employee. Must be unique.</param>
    /// <param name="name">
    /// Name of the Employee. Must be at least 2 characters long and not longer than <see cref="MaxLengthForName"/>
    /// characters.
    /// </param>
    /// <param name="surname">
    /// Surname of the Employee. Must be at least 2 characters long and not longer than <see
    /// cref="MaxLengthForSurname"/> characters.
    /// </param>
    /// <param name="email">Email for the Employee. Must be valid email address.</param>
    /// <exception cref="ArgumentException"></exception>
    private Employee(Guid id, string name, string surname, Email email)
    {
        Validate(name, surname, email);
        Name = name;
        Surname = surname;
        Email = email;
    }

    /// <summary>
    /// Creates new <see cref="Employee"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public static Employee Create(Guid id, string name, string surname, Email email)
    {
        var candidate = new Employee(id: id,
                                      name: name,
                                      surname: surname,
                                      email: email);
        //TODO: consider
        //  candidate.AddDomainEvent(new CandidateCreatedDomainEvent(candidate));

        return candidate;
    }

    private static void Validate(string name, string surname, Email email)
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
        if (email is null)
        {
            throw new ArgumentNullException("Email cannot be null!");
        }
    }

    /// <summary>
    /// Updates main attribute of the <see cref="Employee"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public Employee Update(string name, string surname, Email email)
    {
        Validate(name, surname, email);
        Name = name;
        Surname = surname;
        Email = email;
        return this;
    }
}
