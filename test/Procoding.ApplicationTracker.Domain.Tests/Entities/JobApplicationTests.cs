using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Events;
using Procoding.ApplicationTracker.Domain.Tests.TestData;

namespace Procoding.ApplicationTracker.Domain.Tests.Entities;

[TestFixture]
public class JobApplicationTests
{
    [Test]
    public void Create_ValidParameters_ReturnsJobApplication()
    {
        // Arrange
        var candidate = CandidateTestData.ValidCandidate;
        var id = Guid.NewGuid();
        var jobApplicationSource = JobApplicationSourceTestData.ValidJobApplicationSource;
        var company = CompanyTestData.ValidCompany;
        var timeProvider = Substitute.For<FakeTimeProvider>();
        timeProvider.GetUtcNow().Returns(DateTime.UtcNow);

        // Act
        var jobApplication = JobApplication.Create(candidate, id, jobApplicationSource, company, timeProvider);

        // Assert
        Assert.That(jobApplication, Is.Not.Null);
        Assert.That(jobApplication.Candidate, Is.EqualTo(candidate));
        Assert.That(jobApplication.AppliedOnUTC, Is.EqualTo(timeProvider.GetUtcNow().DateTime));
        Assert.That(jobApplication.ApplicationSource, Is.EqualTo(jobApplicationSource));
        Assert.That(jobApplication.Company, Is.EqualTo(company));
        Assert.That(jobApplication.JobApplicationStatus, Is.EqualTo(JobApplicationStatus.Applied));
    }

    [Test]
    public void CreateNewInterview_ValidParameters_AddsInterviewStep()
    {
        // Arrange
        var jobApplication = JobApplicationTestData.ValidJobApplication;
        var id = Guid.NewGuid();
        var description = "Interview Description";
        var interviewStepType = InterviewStepType.Initial;

        // Act
        var interviewStep = jobApplication.CreateNewInterview(id, description, interviewStepType);

        // Assert
        Assert.That(jobApplication.InterviewSteps, Has.Member(interviewStep));
        Assert.That(jobApplication.InterviewSteps, Has.Count.EqualTo(1));
    }

    [Test]
    public void Create_NewJobApplication_DispatchesJobApplicationCreatedEvent()
    {
        // Arrange && Act
        var jobApplication = JobApplicationTestData.ValidJobApplication;

        // Assert
        Assert.That(jobApplication.DomainEvents, Has.Count.EqualTo(1));
        var domainEvent = jobApplication.DomainEvents.FirstOrDefault();
        Assert.That(domainEvent, Is.InstanceOf<AppliedForAJobDomainEvent>());
        var createdEvent = (AppliedForAJobDomainEvent)domainEvent!;
        Assert.That(createdEvent.JobApplication, Is.EqualTo(jobApplication));
    }

    [Test]
    public void CreateNewInterview_NewInterviewAdded_DispatchesNewInterviewAddedEvent()
    {
        // Arrange
        var jobApplication = JobApplicationTestData.ValidJobApplication;
        var id = Guid.NewGuid();
        var description = "Interview Description";
        var interviewStepType = InterviewStepType.Initial;

        // Act
        jobApplication.CreateNewInterview(id, description, interviewStepType);

        // Assert
        Assert.That(jobApplication.DomainEvents, Has.Count.EqualTo(2));
        var domainEventCreation = jobApplication.DomainEvents.FirstOrDefault();
        Assert.That(domainEventCreation, Is.InstanceOf<AppliedForAJobDomainEvent>());
        var domainEventInterviewAdded = jobApplication.DomainEvents.Skip(1).Take(1).FirstOrDefault();
        Assert.That(domainEventInterviewAdded, Is.InstanceOf<NewInterviewAddedDomainEvent>());
        var interviewAddedEvent = (NewInterviewAddedDomainEvent)domainEventInterviewAdded!;
        Assert.That(interviewAddedEvent.InterviewStep.Description, Is.EqualTo(description));
        Assert.That(interviewAddedEvent.InterviewStep.InteviewStepType, Is.EqualTo(interviewStepType));
    }
}

