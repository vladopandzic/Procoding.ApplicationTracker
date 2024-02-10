﻿using Procoding.ApplicationTracker.Domain.Abstractions;
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
        ValidateNameSurname(name, surname);
        Name = name;
        Surname = surname;
        Email = email;
    }

    private static void ValidateNameSurname(string name, string surname)
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
    }

    /// <summary>
    /// Creates new <see cref="Candidate"/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public static Candidate Create(Guid id, string name, string surname, Email email)
    {
        var candidate = new Candidate(id: id,
                                      name: name,
                                      surname: surname,
                                      email: email);

        candidate.AddDomainEvent(new CandidateCreatedDomainEvent(candidate));

        return candidate;
    }

    /// <summary>
    /// Updates main attribute of the <see cref="Candidate"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="surname"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    public Candidate Update(string name, string surname, Email email)
    {
        ValidateNameSurname(name, surname);
        Name = name;
        Surname = surname;
        Email = email;
        return this;
    }

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
    public Email Email { get; private set; }

    /// <summary>
    /// Job applications for the candidate. Using AsReadOnly() will create a read only wrapper around the private list so is protected against "external updates".
    /// It's much cheaper than .ToList() because it will not have to copy all items in a new collection. (Just one heap alloc for the wrapper instance)
    /// https://msdn.microsoft.com/en-us/library/e78dcd75(v=vs.110).aspx
    /// </summary>
    public IReadOnlyList<JobApplication> JobApplications => _jobApplications.AsReadOnly();

    /// <inheritdoc/>
    public DateTime? DeletedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime CreatedOnUtc { get; }

    /// <inheritdoc/>
    public DateTime ModifiedOnUtc { get; }

    /// <summary>
    ///  Applies for a job for <paramref name="company"/>.
    /// </summary>
    /// <param name="company"></param>
    /// <param name="jobApplicationSource"></param>
    /// <param name="timeProvider"></param>
    /// <returns></returns>
    public JobApplication ApplyForAJob(Company company, JobApplicationSource jobApplicationSource, TimeProvider timeProvider)
    {
        var jobApplication = JobApplication.Create(candidate: this,
                                                   id: Guid.NewGuid(),
                                                   jobApplicationSource: jobApplicationSource,
                                                   company: company,
                                                   timeProvider: timeProvider);

        _jobApplications.Add(jobApplication);

        //TODO: consider removing adding event and also Aggregate root property from JobApplication
        return jobApplication;
    }

    public void NewJobInterviewStep(JobApplication jobApplication, string description, InterviewStepType interviewStepType)
    {
        var interviewStep = jobApplication.CreateNewInterview(id: Guid.NewGuid(),
                                                              description: description,
                                                              inteviewStepType: interviewStepType);


    }
}
