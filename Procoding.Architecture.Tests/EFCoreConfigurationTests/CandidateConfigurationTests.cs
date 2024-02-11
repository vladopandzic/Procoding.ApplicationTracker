using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.Infrastructure.Configurations;
using Procoding.Architecture.Tests.Helpers;
using System.Reflection;

namespace Procoding.Architecture.Tests.EFCoreConfigurationTests;

[TestFixture]
public class CandidateConfigurationTests
{
    [Test]
    public void Candidate_Entity_Configuration_Is_Valid()
    {
        // Arrange
        var modelBuilder = new ModelBuilder(new ConventionSet());
        var entityTypeBuilder = modelBuilder.Entity<Candidate>();

        var configuration = new CandidateConfiguration();

        // Act
        configuration.Configure(entityTypeBuilder);
        var model = modelBuilder.FinalizeModel();
        var entity = model.FindEntityType(typeof(Candidate))!;

        //Assert
        Assert.That(entity, Is.Not.Null, "Entity should not be null");
        Assert.That(entity.GetTableName(), Is.EqualTo("Candidates"));
        Assert.That(EntityTypeConfigurationHelper.PrimaryKey(entity, nameof(Candidate.Id)), Is.True);
        Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(Candidate.Name))!.GetMaxLength, Is.EqualTo(Candidate.MaxLengthForName));
        Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(Candidate.Surname))!.GetMaxLength, Is.EqualTo(Candidate.MaxLengthForSurname));
        Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(Candidate.Surname))?.IsNullable, Is.False);
        Assert.That(EntityTypeConfigurationHelper.ComplexProperty(entity, nameof(Candidate.Email))?.IsNullable, Is.False);
        Assert.That(EntityTypeConfigurationHelper.GetPropertyOfComplexProperty(entity, nameof(Candidate.Email), nameof(Email.Value))?.IsNullable, Is.False);
        Assert.That(EntityTypeConfigurationHelper.GetPropertyOfComplexProperty(entity, nameof(Candidate.Email), nameof(Email.Value))!.GetMaxLength, Is.EqualTo(Email.MaxLengthForValue));
        Assert.That(EntityTypeConfigurationHelper.Navigation(entity, nameof(Candidate.JobApplications))?.ForeignKey.DeleteBehavior, Is.EqualTo(DeleteBehavior.Cascade));
    }
}

