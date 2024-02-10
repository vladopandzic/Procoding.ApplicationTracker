using NUnit.Framework;
using System.Reflection;

namespace Procoding.Architecture.Tests;

[TestFixture]
public class EntityTypeConfigurationTests
{
    [Test]
    public void AllStringProperties_HaveMaxLengthConfigured()
    {
        // Arrange
        var infrastructureAssembly = typeof(ApplicationTracker.Infrastructure.AssemblyReference).Assembly;

        List<Type> types = EntityTypeConfigurationHelper.GetEntityTypeConfigurations(infrastructureAssembly);

        //Act&Assert
        foreach (var type in types)
        {
            MethodInfo? entityMethod = EntityTypeConfigurationHelper.GetConfigureMethod(type);

            Type? entityType = EntityTypeConfigurationHelper.GetEntityType(type);

            if (entityMethod is not null && entityType is not null)
            {
                Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType? entity = EntityTypeConfigurationHelper.GetEntityFrameworkEntityType(type, entityMethod, entityType);

                foreach (var property in entity!.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        var maxLength = property.GetMaxLength();
                        if (maxLength is null)
                        {

                            Assert.Fail($"{type.Name}.{property.Name} has no MaxLength configured.");
                        }
                    }
                }
            }
        }
    }

}