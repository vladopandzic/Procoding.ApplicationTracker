using Procoding.ApplicationTracker.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.Architecture.Tests
{
    internal class PropertyHelper
    {
        public static IEnumerable<PropertyInfo> GetEnumerablePropertiesAssignableFrom<T>(Assembly assembly)
        {
            return assembly.GetTypes()
                           .Where(type => type.IsClass && !type.IsAbstract && typeof(T).IsAssignableFrom(type))
                           .SelectMany(type => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                           .Where(property => IsEnumerablePropertyWithoutStringType(property.PropertyType));
        }

        public static bool IsReadOnlyCollection(Type type)
        {
            var a = type.IsGenericType &&
           (
               type.GetGenericTypeDefinition() == typeof(IReadOnlyCollection<>) ||
               type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>)
           );
            return a;
        }

        public static bool IsEnumerablePropertyWithoutStringType(Type propertyType)
        {
            return propertyType != typeof(string) &&
                   propertyType.GetInterfaces().Any(interfaceType =>
                   interfaceType.IsGenericType &&
                   interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>));
        }

        public static PropertyInfo[] GetPropertiesWithNonPrivateSetters(Assembly assembly)
        {
            var violatingProperties = assembly.GetTypes()
                .Where(type => type.IsClass && !type.IsAbstract && typeof(EntityBase).IsAssignableFrom(type))
                .SelectMany(type => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                .Where(property => property.CanWrite && property.SetMethod != null && property.SetMethod.IsPublic)
                .ToArray();

            return violatingProperties;
        }


    }
}
