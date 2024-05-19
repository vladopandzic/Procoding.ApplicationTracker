using FluentAssertions;
using NetArchTest.Rules;
using NUnit.Framework;
using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Events;
using Procoding.Architecture.Tests.Rules;
using System.Reflection;

namespace Procoding.Architecture.Tests
{
    [TestFixture]
    public class DomainTests
    {
        [Test]
        public void DomainProperties_ShouldHave_PrivateSetters()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            // Act
            var violatingProperties = PropertyHelper.GetPropertiesWithNonPrivateSetters(domainAssembly);

            // Assert
            Assert.That(violatingProperties, Is.Empty, GetFailureMessageForPrivateSettersTest(violatingProperties));

        }


        [Test]
        public void DomainProperties_ShouldBe_IReadOnlyCollection()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            // Act

            var violatingProperties = PropertyHelper.GetEnumerablePropertiesAssignableFrom<EntityBase>(domainAssembly)
                                                    .Where(property => !PropertyHelper.IsReadOnlyCollection(property.PropertyType))
                                                    .ToArray();

            // Assert
            Assert.That(violatingProperties, Is.Empty, GetFailureMessageForReadOnlyCollectionTest(violatingProperties));
        }

        [Test]
        public void DomainProperties_ShouldBe_Immutable()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            //Act
            var testResult = Types.InAssembly(domainAssembly)
                                  .That()
                                  .Inherit(typeof(EntityBase))
                                  .Should()
                                  .BeImmutable()
                                  .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void BaseEntity_Should_Have_Private_Parameterless_Constructor()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            //Act
            var testResult = Types.InAssembly(domainAssembly)
                                  .That()
                                  .Inherit(typeof(EntityBase))
                                  .And()
                                  .AreNotAbstract()
                                  .Should()
                                  .MeetCustomRule(new HaveParametersConstructor())
                                  .GetResult();


            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void BaseEntity_Classes_Should_Be_Sealed()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            //Act
            var testResult = Types.InAssembly(domainAssembly)
                                  .That()
                                  .Inherit(typeof(EntityBase))
                                  .And()
                                  .AreNotAbstract()
                                  .Should()
                                  .BeSealed()
                                  .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void DomainEvents_Should_Be_Sealed()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            //Act
            var testResult = Types.InAssembly(domainAssembly)
                                  .That()
                                  .ImplementInterface(typeof(IDomainEvent))
                                  .Should()
                                  .BeSealed()
                                  .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void DomainEvents_Should_Be_Imuttable()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            //Act
            var testResult = Types.InAssembly(domainAssembly)
                                  .That()
                                  .ImplementInterface(typeof(IDomainEvent))
                                  .Should()
                                  .BeImmutable()
                                  .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void DomainEvents_Should_HaveDomainEventPostfix()
        {
            // Arrange
            var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            //Act
            var testResult = Types.InAssembly(domainAssembly)
                                  .That()
                                  .ImplementInterface(typeof(IDomainEvent))
                                  .Should()
                                  .HaveNameEndingWith("DomainEvent")
                                  .GetResult();

            // Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        private string GetFailureMessageForReadOnlyCollectionTest(PropertyInfo[] violatingProperties)
        {
            var message = "The following domain properties are not of type IReadOnlyList:\n";
            foreach (var property in violatingProperties)
            {
                message += $"{property.DeclaringType?.Name}.{property.Name}\n";
            }
            return message;
        }

        private string GetFailureMessageForPrivateSettersTest(PropertyInfo[] violatingProperties)
        {
            var message = "The following domain properties do not have private setters:\n";
            foreach (var property in violatingProperties)
            {
                message += $"{property.DeclaringType?.Name}.{property.Name}\n";
            }
            return message;
        }

    }
}
