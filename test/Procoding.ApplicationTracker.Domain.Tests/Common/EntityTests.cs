using FluentAssertions;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Tests.Common
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void Equals_ShouldReturnFalse_WhenOtherEntityIsNull()
        {
            // Arrange
            Company entity = CompanyTestData.ValidCompany;

            // Act
            bool result = entity.Equals(null);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenOtherEntityIsDifferentType()
        {
            // Arrange
            Company entity = CompanyTestData.ValidCompany;

            // Act
            bool result = entity.Equals(JobApplicationSourceTestData.ValidJobApplicationSource);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenOtherEntityIsTheSameEntity()
        {
            // Arrange
            Company entity = CompanyTestData.ValidCompany;

            // Act
            bool result = entity.Equals(entity);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenOtherObjectIsNull()
        {
            // Arrange
            Company entity = CompanyTestData.ValidCompany;

            // Act
            bool result = entity.Equals((object?)null);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void Equals_ShouldReturnTrue_WhenOtherObjectIsTheSameEntity()
        {
            // Arrange
            Company entity = CompanyTestData.ValidCompany;

            // Act
            bool result = entity.Equals((object)entity);

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void Equals_ShouldReturnFalse_WhenOtherObjectIsNotAnEntity()
        {
            // Arrange
            Company entity = CompanyTestData.ValidCompany;

            // Act
            bool result = entity.Equals(JobApplicationSourceTestData.ValidJobApplicationSource);

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void GetHashCode_ShouldReturnProperHashCode()
        {
            // Arrange
            Company entity = CompanyTestData.ValidCompany;

            // Act
            int hashcode = entity.GetHashCode();

            // Assert
            hashcode.Should().Be(entity.Id.GetHashCode() * 41);
        }

        [Test]
        public void EqualityOperator_ShouldReturnTrue_WhenBothEntitiesAreNull()
        {
            // Arrange
            Company? entity1 = null;
            Company? entity2 = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            bool? result = entity1 == entity2;
#pragma warning restore CS8604 // Possible null reference argument.

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenFirstEntityIsNull()
        {
            // Arrange
            Company entity1 = CompanyTestData.ValidCompany;
            Company? entity2 = null;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            bool result = entity1 == entity2;
#pragma warning restore CS8604 // Possible null reference argument.

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void EqualityOperator_ShouldReturnFalse_WhenSecondEntityIsNull()
        {
            // Arrange
            Company? entity1 = null;
            Company entity2 = CompanyTestData.ValidCompany;

            // Act
#pragma warning disable CS8604 // Possible null reference argument.
            bool result = entity1 == entity2;
#pragma warning restore CS8604 // Possible null reference argument.

            // Assert
            result.Should().BeFalse();
        }

        [Test]
        public void EqualityOperator_ShouldReturnTrue_WhenEntitiesAreEqual()
        {
            // Arrange
            Company? entity1 = CompanyTestData.ValidCompany;
            Company entity2 = entity1;

            // Act
            bool result = entity1 == entity2;

            // Assert
            result.Should().BeTrue();
        }

        [Test]
        public void UnEqualityOperator_ShouldReturnTrue_WhenEntitiesAreNotEqual()
        {
            // Arrange
            Company entity1 = CompanyTestData.ValidCompany;
            Company entity2 = CompanyTestData.ValidCompany;

            // Act
            bool result = entity1 != entity2;

            // Assert
            result.Should().BeTrue();
        }
    }
}
