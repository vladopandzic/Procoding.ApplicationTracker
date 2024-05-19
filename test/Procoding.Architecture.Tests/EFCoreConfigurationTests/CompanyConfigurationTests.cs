using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.Infrastructure.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Procoding.Architecture.Tests.Helpers;

namespace Procoding.Architecture.Tests.EFCoreConfigurationTests
{
    [TestFixture]
    public class CompanyConfigurationTests
    {
        [Test]
        public void Company_Entity_Configuration_Is_Valid()
        {
            // Arrange
            var modelBuilder = new ModelBuilder(new ConventionSet());
            var entityTypeBuilder = modelBuilder.Entity<Company>();

            var configuration = new CompanyConfiguration();

            // Act
            configuration.Configure(entityTypeBuilder);
            var model = modelBuilder.FinalizeModel();
            var entity = model.FindEntityType(typeof(Company))!;

            //Assert
            Assert.That(entity, Is.Not.Null, "Entity should not be null");
            Assert.That(entity.GetTableName(), Is.EqualTo("Companies"));
            Assert.That(EntityTypeConfigurationHelper.PrimaryKey(entity, nameof(Company.Id)), Is.True);
            Assert.That(EntityTypeConfigurationHelper.ComplexProperty(entity, nameof(Company.OfficialWebSiteLink))?.IsNullable, Is.False);
            Assert.That(EntityTypeConfigurationHelper.ComplexProperty(entity, nameof(Company.CompanyName))?.IsNullable, Is.False);
            Assert.That(EntityTypeConfigurationHelper.GetPropertyOfComplexProperty(entity, nameof(Company.OfficialWebSiteLink), nameof(Link.Value))?.IsNullable, Is.False);
            Assert.That(EntityTypeConfigurationHelper.GetPropertyOfComplexProperty(entity, nameof(Company.CompanyName), nameof(CompanyName.Value))!.IsNullable, Is.False);
            Assert.That(EntityTypeConfigurationHelper.Navigation(entity, nameof(Company.CompanyAverageGrossSalaries))?.ForeignKey.DeleteBehavior, Is.EqualTo(DeleteBehavior.Cascade));

        }
    }
}
