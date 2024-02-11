using NetArchTest.Rules;
using NUnit.Framework;
using Procoding.Architecture.Tests.Helpers;
using System.Reflection;

namespace Procoding.Architecture.Tests;

[TestFixture]
public class RepositoriesTests
{
    [Test]
    public void Repositories_AsyncMethods_ShouldEndWithAsync()
    {
        // Arrange
        var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

        //Act
        foreach (var type in domainAssembly.GetTypes())
        {
            if (type.Name.EndsWith("Repository"))
            {
                var word = "Async";

                IEnumerable<MethodInfo> incorrectlyNamedMethods = MethodsNamingHelper.GetAsyncMethodsThatDontEndWith(type, word);

                foreach (var method in incorrectlyNamedMethods)
                {
                    Assert.Fail($"Method '{method.Name}' in type '{type.FullName}' does not end with 'Async'.");
                }
            }
        }

        //Assert
        Assert.Pass();
    }

    [Test]
    public void Repositories_AsyncMethods_ShouldAcceptCancellationToken()
    {
        // Arrange
        var domainAssembly = typeof(ApplicationTracker.Domain.AssemblyReference).Assembly;

        //Act
        foreach (var type in domainAssembly.GetTypes())
        {
            if (type.Name.EndsWith("Repository"))
            {
                IEnumerable<MethodInfo> incorrectlyNamedMethods = MethodsNamingHelper.GetAsyncMethodsThatDontAcceptCancellationToken(type);

                foreach (var method in incorrectlyNamedMethods)
                {
                    Assert.Fail($"Method '{method.Name}' in type '{type.FullName}' does not accept CancellationToken.");
                }
            }
        }

        //Assert
        Assert.Pass();
    }
}
