using FluentAssertions;
using FluentValidation;
using NetArchTest.Rules;
using NUnit.Framework;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Procoding.Architecture.Tests;

[TestFixture]
public class ApplicationHandlerTests
{
    [Test]
    public void Commands_ShouldImplementICommand()
    {
        // Arrange
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        //Act
        var testResult = Types.InAssembly(applicationAssembly)
                                .That()
                                .ResideInNamespace("Procoding.ApplicationTracker.Application")
                                .And()
                                .HaveNameEndingWith("Command")
                                .And()
                                .AreNotAbstract()
                                .Should()
                                .ImplementInterface(typeof(ICommand<>))
                                .Or()
                                .ImplementInterface(typeof(ICommand))
                                .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void Handlers_ShouldImplementICommandHandler()
    {
        // Arrange
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        //Act
        var testResult = Types.InAssembly(applicationAssembly)
                                .That()
                                .ResideInNamespace("Procoding.ApplicationTracker.Application")
                                .And()
                                .HaveNameEndingWith("Handler")
                                .And()
                                .AreNotAbstract()
                                .Should()
                                .ImplementInterface(typeof(ICommandHandler<,>))
                                .Or()
                                .ImplementInterface(typeof(ICommandHandler<>))
                                .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void Validators_ShouldImplementICommandHandler()
    {
        // Arrange
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        //Act
        var testResult = Types.InAssembly(applicationAssembly)
                                .That()
                                .ResideInNamespace("Procoding.ApplicationTracker.Application")
                                .And()
                                .HaveNameEndingWith("Validator")
                                .And()
                                .AreNotAbstract()
                                .Should()
                                .Inherit(typeof(AbstractValidator<>))
                                .GetResult();

        // Assert
        testResult.IsSuccessful.Should().BeTrue();
    }


    [Test]
    public void EachCommand_ShouldHaveCorrespondingHandler()
    {
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        var commandTypes = applicationAssembly.GetTypes()
            .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith("Procoding.ApplicationTracker.Application") && t.Name.EndsWith("Command"));

        foreach (var commandType in commandTypes)
        {
            var handlerName = commandType.Name.Replace("Command", "CommandHandler");
            var handlerType = applicationAssembly.GetTypes().FirstOrDefault(t => t.Name == handlerName);
            handlerType.Should().NotBeNull($"because command '{commandType.FullName}' should have a corresponding handler '{handlerName}' whose name end with CommandHandler");
        }
    }

    [Test]
    public void EachCommand_ShouldHaveCorrespondingValidator()
    {
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        var commandTypes = applicationAssembly.GetTypes()
            .Where(t => t.IsClass && t.Namespace != null && t.Namespace.StartsWith("Procoding.ApplicationTracker.Application") && t.Name.EndsWith("Command"));

        foreach (var commandType in commandTypes)
        {
            var validatorName = commandType.Name.Replace("Command", "CommandValidator");
            var validatorType = applicationAssembly.GetTypes().FirstOrDefault(t => t.Name == validatorName);
            validatorType.Should().NotBeNull($"because command '{commandType.FullName}' should have a corresponding validator '{validatorName} whose name end with CommandValidator'");
        }
    }

    [Test]
    public void Types_ShouldBeSealed()
    {
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        var testResult = Types.InAssembly(applicationAssembly)
                                .That()
                                .ResideInNamespace("Procoding.ApplicationTracker.Application")
                                .And()
                                .AreNotInterfaces()
                                .And()
                                .HaveNameEndingWith("Command").Or()
                                .HaveNameEndingWith("CommandHandler").Or()
                                .HaveNameEndingWith("CommandValidator")
                                .And()
                                .AreNotAbstract()
                                .And()
                                .AreNotInterfaces()
                                .Should()
                                .BeSealed()
                                .GetResult();

        testResult.IsSuccessful.Should().BeTrue();
    }

    [Test]
    public void CommandHandlers_ShouldBeInternal()
    {
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        var handlerTypes = applicationAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Handler") && t.Namespace != null && t.Namespace.StartsWith("Procoding.ApplicationTracker.Application"));

        foreach (var handlerType in handlerTypes)
        {
            handlerType.IsNotPublic.Should().BeTrue($"because {handlerType.FullName} should be internal");
        }
    }

    [Test]
    public void Commands_ShouldBePublic()
    {
        var applicationAssembly = typeof(ApplicationTracker.Application.AssemblyReference).Assembly;

        var commandTypes = applicationAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Command") && t.Namespace != null && t.Namespace.StartsWith("Procoding.ApplicationTracker.Application"));

        foreach (var commandType in commandTypes)
        {
            commandType.IsPublic.Should().BeTrue($"because {commandType.FullName} should be public");
        }
    }
}
