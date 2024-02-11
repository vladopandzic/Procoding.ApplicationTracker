using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Infrastructure.Configurations;
using Procoding.Architecture.Tests.Helpers;

namespace Procoding.Architecture.Tests.EFCoreConfigurationTests;

[TestFixture]
public class JobApplicationConfiguratorTests
{
    [Test]
    public void JobApplication_Entity_Configuration_Is_Valid()
    {
        // Arrange
        var modelBuilder = new ModelBuilder(new ConventionSet());
        var entityTypeBuilder = modelBuilder.Entity<JobApplication>();

        var configuration = new JobApplicationConfiguration();

        // Act
        configuration.Configure(entityTypeBuilder);
        var model = modelBuilder.FinalizeModel();
        var entity = model.FindEntityType(typeof(JobApplication))!;

        //Assert
        Assert.That(entity, Is.Not.Null, "Entity should not be null");
        Assert.That(entity.GetTableName(), Is.EqualTo("JobApplications"));
        Assert.That(EntityTypeConfigurationHelper.PrimaryKey(entity, nameof(JobApplication.Id)), Is.True);
        Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(JobApplication.AppliedOnUTC))!.IsNullable, Is.False);
        Assert.That(EntityTypeConfigurationHelper.Navigation(entity, nameof(JobApplication.InterviewSteps))?.ForeignKey.DeleteBehavior, Is.EqualTo(DeleteBehavior.Cascade));
    }
}

