using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using System;
using Procoding.ApplicationTracker.Domain.Tests.TestData;

namespace Procoding.ApplicationTracker.Domain.Tests.Entities;

[TestFixture]
public class InterviewStepTypeTests
{
    [Test]
    public void Constructor_ValidParameters_ObjectCreated()
    {
        // Arrange
        var jobApplication = JobApplicationTestData.ValidJobApplication;
        var id = Guid.NewGuid();
        var description = "Test Description";
        var interviewStepType = InterviewStepType.Initial;

        // Act
        var interviewStep = new InterviewStep(jobApplication, id, description, interviewStepType);

        // Assert
        Assert.That(interviewStep, Is.Not.Null);
        Assert.That(interviewStep.Description, Is.EqualTo(description));
        Assert.That(interviewStep.JobApplication, Is.EqualTo(jobApplication));
        Assert.That(interviewStep.InteviewStepType, Is.EqualTo(interviewStepType));
    }

    [Test]
    public void Constructor_DescriptionTooLong_ArgumentExceptionThrown()
    {
        // Arrange
        var jobApplication = JobApplicationTestData.ValidJobApplication;
        var id = Guid.NewGuid();
        var description = new string('x', InterviewStep.MaxLengthForDescription + 1);
        var interviewStepType = InterviewStepType.Initial;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new InterviewStep(jobApplication, id, description, interviewStepType));
    }

    [Test]
    public void Constructor_NullJobApplication_ArgumentNullExceptionThrown()
    {
        // Arrange
        JobApplication? jobApplication = null!;
        var id = Guid.NewGuid();
        var description = "Test Description";
        var interviewStepType = InterviewStepType.Initial;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new InterviewStep(jobApplication, id, description, interviewStepType));
    }

    [Test]
    public void Constructor_NullDescription_ArgumentNullExceptionThrown()
    {
        // Arrange
        var jobApplication = JobApplicationTestData.ValidJobApplication;
        var id = Guid.NewGuid();
        string? description = null;
        var interviewStepType = InterviewStepType.Initial;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new InterviewStep(jobApplication, id, description!, interviewStepType));
    }
}

