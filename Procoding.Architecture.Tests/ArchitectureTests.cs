using FluentAssertions;
using NetArchTest.Rules;
using NUnit.Framework;
namespace Procoding.Architecture.Tests
{
    [TestFixture]
    public class ArchitectureTests
    {
        private const string DomainNamespace = "ApplicationTracker.Domain";
        private const string ApplicationNamespace = "ApplicationTracker.Application";
        private const string InfrastructureNamespace = "ApplicationTracker.Infrastructure";
        private const string WebNamespace = "ApplicationTracker.Web";
        private const string WebRootNamespace = "ApplicationTracker.WebRoot";

        [Test]
        public void Domain_Should_Not_Have_Dependency_On_Other_Projects()
        {
            //Arrange
            var assembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

            var otherProjects = new[] {
                ApplicationNamespace, InfrastructureNamespace, WebNamespace, WebRootNamespace
            };
            //Act
            var testResult = Types.InAssembly(assembly)
                                  .ShouldNot()
                                  .HaveDependencyOnAll(otherProjects)
                                  .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void Application_Should_Not_Have_Dependency_On_Other_Projects()
        {
            //Arrange
            var assembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

            var otherProjects = new[] {
                InfrastructureNamespace, WebNamespace, WebRootNamespace
            };

            //Act
            var testResult = Types.InAssembly(assembly)
                                  .ShouldNot()
                                  .HaveDependencyOnAll(otherProjects)
                                  .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }


        [Test]
        public void Infrastructure_Should_Not_Have_Dependency_On_Other_Projects()
        {
            //Arrange
            var assembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

            var otherProjects = new[] {
                WebNamespace, WebRootNamespace
            };
            //Act
            var testResult = Types.InAssembly(assembly)
                                  .ShouldNot()
                                  .HaveDependencyOnAll(otherProjects)
                                  .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }

        [Test]
        public void Web_Should_Not_Have_Dependency_On_Other_Projects()
        {
            //Arrange
            var assembly = typeof(ApplicationTracker.Web.AssemblyReference).Assembly;

            var otherProjects = new[] {
                InfrastructureNamespace, WebRootNamespace,
            };
            //Act
            var testResult = Types.InAssembly(assembly)
                                  .ShouldNot()
                                  .HaveDependencyOnAll(otherProjects)
                                  .GetResult();

            //Assert
            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}
