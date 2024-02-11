using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Events;

namespace Procoding.ApplicationTracker.Domain.Tests.Entities;

[TestFixture]
public class JobApplicationSourceTests
{
    [Test]
    public void Create_ValidArguments_ReturnsJobApplicationSource()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Test Name";

        // Act
        var jobApplicationSource = JobApplicationSource.Create(id, name);

        // Assert
        Assert.That(jobApplicationSource, Is.Not.Null);
        Assert.That(jobApplicationSource.Id, Is.EqualTo(id));
        Assert.That(jobApplicationSource.Name, Is.EqualTo(name));
    }

    [Test]
    public void Create_NullName_ThrowsArgumentException()
    {
        // Arrange && Act & Assert
        Assert.Throws<ArgumentNullException>(() => { _ = TestData.JobApplicationSourceTestData.InvalidJobApplicationSourceWithNullEmail; });
    }

    [Test]
    public void Create_EmptyName_ThrowsArgumentException()
    {
        // Arrange && Act & Assert
        Assert.Throws<ArgumentException>(() => { _ = TestData.JobApplicationSourceTestData.InvalidJobApplicationSourceWithEmptyEmail; });
    }

    [Test]
    public void Create_NameTooLong_ThrowsArgumentException()
    {
        //Arrange && Act & Assert
        Assert.Throws<ArgumentException>(() => { _ = TestData.JobApplicationSourceTestData.InvalidJobApplicationSourceWithToLongEmail; });
    }

    [Test]
    public void Create_NameTooShort_ThrowsArgumentException()
    {
        //Arrange && Act & Assert
        Assert.Throws<ArgumentException>(() => { _ = TestData.JobApplicationSourceTestData.InvalidJobApplicationSourceWithToShortEmail; });
    }

    [Test]
    public void ChangeName_ValidName_UpdatesName()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string originalName = "Original Name";
        string newName = "New Name";
        var jobApplicationSource = JobApplicationSource.Create(id, originalName);

        // Act
        jobApplicationSource.ChangeName(newName);

        // Assert
        Assert.That(jobApplicationSource.Name, Is.EqualTo(newName));
    }

    [Test]
    public void ChangeName_NullName_ThrowsArgumentException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Original Name";
        var jobApplicationSource = JobApplicationSource.Create(id, name);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => jobApplicationSource.ChangeName(null!));
    }

    [Test]
    public void ChangeName_WhitespaceName_ThrowsArgumentException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Original Name";
        var jobApplicationSource = JobApplicationSource.Create(id, name);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => jobApplicationSource.ChangeName("   "));
    }

    [Test]
    public void ChangeName_NameTooShort_ThrowsArgumentException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string originalName = "Original Name";
        var jobApplicationSource = JobApplicationSource.Create(id, originalName);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => jobApplicationSource.ChangeName(""));
    }

    [Test]
    public void ChangeName_NameTooLong_ThrowsArgumentException()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string originalName = "Original Name";
        var jobApplicationSource = JobApplicationSource.Create(id, originalName);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => jobApplicationSource.ChangeName(new string('x', JobApplicationSource.MaxLengthForName + 1)));
    }

    [Test]
    public void ChangeName_SameName_NoChangeEvent()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string name = "Original Name";
        var jobApplicationSource = JobApplicationSource.Create(id, name);

        // Act
        jobApplicationSource.ChangeName(name);

        // Assert
        Assert.That(jobApplicationSource.DomainEvents, Has.Count.EqualTo(1));
        var domainEvent = jobApplicationSource.DomainEvents.FirstOrDefault();
        Assert.That(domainEvent, Is.InstanceOf<JobApplicationSourceCreatedDomainEvent>());
        var updatedEvent = (JobApplicationSourceCreatedDomainEvent)domainEvent!;
        Assert.That(updatedEvent.JobApplicationSource, Is.EqualTo(jobApplicationSource));
    }

    [Test]
    public void ChangeName_NameDifferent_EventAdded()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        string originalName = "Original Name";
        string newName = "New Name";
        var jobApplicationSource = JobApplicationSource.Create(id, originalName);

        // Act
        jobApplicationSource.ChangeName(newName);

        // Assert
        Assert.That(jobApplicationSource.DomainEvents, Has.Count.EqualTo(2));
        var domainEvent = jobApplicationSource.DomainEvents.Skip(1).Take(1).FirstOrDefault();
        Assert.That(domainEvent, Is.InstanceOf<JobApplicationSourceUpdatedDomainEvent>());
        var updatedEvent = (JobApplicationSourceUpdatedDomainEvent)domainEvent!;
        Assert.That(updatedEvent.Id, Is.EqualTo(id));
        Assert.That(updatedEvent.NewName, Is.EqualTo(newName));
    }
}

