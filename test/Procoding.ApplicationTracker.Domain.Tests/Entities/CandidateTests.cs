using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Events;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.Tests.TestData;
using Procoding.ApplicationTracker.Domain.ValueObjects;

namespace Procoding.ApplicationTracker.Domain.Tests.Entities;

[TestFixture]
public class CandidateTests
{
    [Test]
    public void Create_WithValidArguments_CreatesCandidate()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "John";
        var surname = "Doe";
        var email = new Email("john@example.com");
        var password = "test123";

        // Act
        var candidate = Candidate.Create(id: id, name: name, surname: surname, email: email, password: password, new FakePasswordHasher<Candidate>());

        // Assert
        Assert.That(candidate, Is.Not.Null);
        Assert.That(candidate.Id, Is.EqualTo(id));
        Assert.That(candidate.Name, Is.EqualTo(name));
        Assert.That(candidate.Surname, Is.EqualTo(surname));
        Assert.That(candidate.Email, Is.EqualTo(email));
    }

    [Test]
    public void Create_WithEmptyName_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "";
        var surname = "Doe";
        var email = new Email("john@example.com");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Candidate.Create(id: id, name: name, surname: surname, email: email, "", new FakePasswordHasher<Candidate>()));
    }

    [Test]
    public void Create_WithEmptySurname_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "John";
        var surname = "";
        var email = new Email("john@example.com");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => Candidate.Create(id: id, name: name, surname: surname, email: email, "", new FakePasswordHasher<Candidate>()));
    }

    [Test]
    public void Create_WithInvalidEmail_ThrowsArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "John";
        var surname = "Doe";

        // Act & Assert
        Assert.Throws<InvalidEmailException>(() => Candidate.Create(id: id,
                                                                    name: name,
                                                                    surname: surname,
                                                                    email: new Email("invalid-email"),
                                                                    "",
                                                                    new FakePasswordHasher<Candidate>()));
    }

    [Test]
    public void Create_WithValidArguments_DispatchesCandidateCreatedEvent()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "John";
        var surname = "Doe";
        var email = new Email("john@example.com");

        // Act
        var candidate = Candidate.Create(id, name, surname, email, "", new FakePasswordHasher<Candidate>());

        // Assert
        Assert.That(candidate.DomainEvents, Has.Count.EqualTo(1));
        var dispatchedEvent = candidate.DomainEvents.First();
        Assert.That(dispatchedEvent, Is.InstanceOf<CandidateCreatedDomainEvent>());
        var createdEvent = (CandidateCreatedDomainEvent)dispatchedEvent;
        Assert.That(createdEvent.Candidate, Is.EqualTo(candidate));
    }

    [Test]
    public void Create_WithEmptyName_DoesNotDispatchAnyEvent()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "";
        var surname = "Doe";
        var email = new Email("john@example.com");
        Candidate? candidate = null;
        // Act
        Assert.Throws<ArgumentException>(() => candidate = Candidate.Create(id, name, surname, email, "", new FakePasswordHasher<Candidate>()));

        // Assert
        Assert.That(candidate, Is.Null);
    }

    [Test]
    public void ApplyForAJob_DispatchesAppliedForAJobDomainEvent()
    {
        // Arrange
        var candidate = CandidateTestData.ValidCandidate;
        var company = CompanyTestData.ValidCompany;
        var jobApplicationSource = JobApplicationSourceTestData.ValidJobApplicationSource;
        var timeProvider = Substitute.For<FakeTimeProvider>();
        timeProvider.GetUtcNow().Returns(DateTime.UtcNow);

        // Act
        candidate.ApplyForAJob(company: company,
                               jobApplicationSource: jobApplicationSource,
                               timeProvider: timeProvider,
                               jobPositionTitle: "Senior .NET sw engineer",
                               jobAdLink: new Link("https://www.link2.com"),
                               workLocationType: WorkLocationType.Remote,
                               jobType: JobType.FullTime);

        // Assert
        Assert.That(candidate.JobApplications.First().DomainEvents, Has.Count.EqualTo(1));
        var dispatchedEvent = candidate.JobApplications.First().DomainEvents.First();
        Assert.That(dispatchedEvent, Is.InstanceOf<AppliedForAJobDomainEvent>());
        var appliedEvent = (AppliedForAJobDomainEvent)dispatchedEvent;
        Assert.That(appliedEvent.JobApplication, Is.EqualTo(candidate.JobApplications.First()));
    }
}
