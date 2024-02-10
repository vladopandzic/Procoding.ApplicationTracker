using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NUnit.Framework.Internal;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using System.Linq.Expressions;
using System.Reflection;

namespace Procoding.Architecture.Tests
{
    internal class EntityTypeConfigurationHelper
    {
        public static List<Type> GetEntityTypeConfigurations(Assembly infrastructureAssembly)
        {
            return infrastructureAssembly.GetTypes()
                                         .Where(t => t.GetInterfaces()
                                                         .Any(i => i.IsGenericType &&
                                                                   i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                                         .ToList();
        }

        public static IMutableEntityType? GetEntityFrameworkEntityType(Type type, MethodInfo entityMethod, Type entityType)
        {
            var modelBuilderInstance = new ModelBuilder();
            return GetEntityFrameworkEntityType(type, entityMethod, entityType, modelBuilderInstance);
        }

        public static IMutableEntityType? GetEntityFrameworkEntityType(Type type, MethodInfo entityMethod, Type entityType, ModelBuilder modelBuilderInstance)
        {
            var modelBuilderEntityMethod = typeof(ModelBuilder).GetMethod("Entity", Type.EmptyTypes)!.MakeGenericMethod(entityType);
            var entityTypeBuilder = modelBuilderEntityMethod.Invoke(modelBuilderInstance, null);

            var entityInstance = Activator.CreateInstance(type);
            entityMethod.Invoke(entityInstance, new object[] { entityTypeBuilder! });

            var entity = modelBuilderInstance.Model.FindEntityType(entityType);
            return entity;
        }

        public static Type? GetEntityType(Type type)
        {
            return type.GetInterfaces()
                     .Where(i => i.IsGenericType &&
                                 i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                     .Select(i => i.GenericTypeArguments[0])
                     .FirstOrDefault();
        }

        public static MethodInfo? GetConfigureMethod(Type type)
        {
            return type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                 .Where(m => m.Name.EndsWith(".Configure")).FirstOrDefault();
        }

        public static IProperty? Property(IEntityType entity, string propertyName)
        {
            return entity.GetProperties().FirstOrDefault(p => p.Name == propertyName);
        }

        public static IProperty? Property2<TEntity, TProperty>(IEntityType entity, TEntity ent, Expression<Func<TEntity, TProperty>> propertyExpression)
        where TEntity : class
        {
            if (!(propertyExpression.Body is MemberExpression memberExpression))
            {
                throw new ArgumentException("Expression is not a member access expression.", nameof(propertyExpression));
            }

            var propertyName = memberExpression.Member.Name;
            return entity.FindProperty(propertyName);
        }

        public static IComplexProperty? ComplexProperty(IEntityType entity, string propertyName)
        {
            return entity.GetComplexProperties().FirstOrDefault(p => p.Name == propertyName);
        }
        public static INavigation? Navigation(IEntityType entity, string propertyName)
        {
            return entity.GetNavigations().FirstOrDefault(p => p.Name == propertyName);
        }

        public static bool PrimaryKey(IEntityType entity, string primaryKeyName)
        {
            return entity.FindPrimaryKey()!.Properties.Any(p => p.Name == primaryKeyName);
        }

        public static IProperty? GetPropertyOfComplexProperty(IEntityType entity, string complexPropertyName, string nameOfPropertyInsideComplexProperty)
        {
            var complexProperty = entity.GetComplexProperties().FirstOrDefault(p => p.Name == complexPropertyName);

            return complexProperty?.ComplexType.GetProperty(nameOfPropertyInsideComplexProperty);
        }
    }
}
