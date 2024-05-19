using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
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
    public class CompanyAverageGrossSalaryConfiguratorTests
    {
        [Test]
        public void CompanyAverageGrossSalary_Entity_Configuration_Is_Valid()
        {
            // Arrange
            var modelBuilder = new ModelBuilder(new ConventionSet());
            var entityTypeBuilder = modelBuilder.Entity<CompanyAverageGrossSalary>();

            var configuration = new CompanyAverageGrossSalaryConfigurator();

            // Act
            configuration.Configure(entityTypeBuilder);
            var model = modelBuilder.FinalizeModel();
            var entity = model.FindEntityType(typeof(CompanyAverageGrossSalary))!;

            //Assert
            Assert.That(entity, Is.Not.Null, "Entity should not be null");
            Assert.That(entity.GetTableName(), Is.EqualTo("CompanyAverageGrossSalaries"));
            Assert.That(EntityTypeConfigurationHelper.PrimaryKey(entity, nameof(CompanyAverageGrossSalary.Id)), Is.True);
            Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(CompanyAverageGrossSalary.Year))?.IsNullable, Is.False);
            Assert.That(EntityTypeConfigurationHelper.Property(entity, nameof(CompanyAverageGrossSalary.GrossSalary))?.IsNullable, Is.False);

        }
    }
}
