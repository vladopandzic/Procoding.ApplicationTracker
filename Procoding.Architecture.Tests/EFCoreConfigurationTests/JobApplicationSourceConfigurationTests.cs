using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Infrastructure.Configurations;
using System.Reflection;

namespace Procoding.Architecture.Tests.EFCoreConfigurationTests;

[TestFixture]
public class JobApplicationSourceConfigurationTests
{
    [Test]
    public void JobApplicationSource_Entity_Configuration_Is_Valid()
    {
        // Arrange
        var modelBuilder = new ModelBuilder(new ConventionSet());
        var entityTypeBuilder = modelBuilder.Entity<JobApplicationSource>();

        var configuration = new JobApplicationSourceConfiguration();

        // Act
        configuration.Configure(entityTypeBuilder);
        var model = modelBuilder.FinalizeModel();
        var entity = model.FindEntityType(typeof(JobApplicationSource))!;

        //Assert
        Assert.That(entity, Is.Not.Null, "Entity should not be null");
        Assert.That(entity.GetTableName(), Is.EqualTo("JobApplicationSources"));
        Assert.That(EntityTypeConfigurationHelper.PrimaryKey(entity, nameof(JobApplicationSource.Id)), Is.True);
        Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(JobApplicationSource.Name))!.GetMaxLength, Is.EqualTo(JobApplicationSource.MaxLengthForName));
        Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(JobApplicationSource.Name))?.IsNullable, Is.False);
    }
}
