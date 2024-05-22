using Ardalis.Specification;
using Procoding.ApplicationTracker.Application.Core.Query;
using System.Linq.Expressions;
using System.Reflection;

public static class SpecificationBuilderExtensions
{
    public static ISpecificationBuilder<T> ApplyFilters<T>(this ISpecificationBuilder<T> specificationBuilder, IEnumerable<Filter> filters)
    {
        if (specificationBuilder is null)
        {
            throw new ArgumentNullException(nameof(specificationBuilder));
        }

        if (filters is null || !filters.Any())
        {
            return specificationBuilder;
        }

        foreach (var filter in filters)
        {
            if (string.IsNullOrEmpty(filter.Key) || filter.Value is null)
            {
                continue;
            }

            var propertyInfo = typeof(T).GetProperty(filter.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo is null)
            {
                continue;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);
            var constant = Expression.Constant(filter.Value);
            Expression? filterExpression = null;


            // Handle value objects (e.g., Email.Value)
            if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string))
            {
                var underlyingPropertyInfo = propertyInfo.PropertyType.GetProperty("Value", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (underlyingPropertyInfo is not null)
                {
                    property = Expression.Property(property, underlyingPropertyInfo);
                }
            }


            switch (filter.Operator?.ToLower())
            {
                case "equals":
                    filterExpression = Expression.Equal(property, constant);
                    break;

                case "not equals":
                    filterExpression = Expression.NotEqual(property, constant);
                    break;

                case "contains":
                    if (propertyInfo.PropertyType == typeof(string) || property.Type == typeof(string))
                    {
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        if (containsMethod is not null)
                        {
                            filterExpression = Expression.Call(property, containsMethod, constant);
                        }
                    }
                    break;

                case "not contains":
                    if (propertyInfo.PropertyType == typeof(string) || property.Type == typeof(string))
                    {
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        if (containsMethod is not null)
                        {
                            var containsCall = Expression.Call(property, containsMethod, constant);
                            filterExpression = Expression.Not(containsCall);
                        }
                    }
                    break;

                case "starts with":
                    if (propertyInfo.PropertyType == typeof(string) || property.Type == typeof(string))
                    {
                        var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                        if (startsWithMethod is not null)
                        {
                            filterExpression = Expression.Call(property, startsWithMethod, constant);
                        }
                    }
                    break;

                case "ends with":
                    if (propertyInfo.PropertyType == typeof(string) || property.Type == typeof(string))
                    {
                        var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                        if (endsWithMethod is not null)
                        {
                            filterExpression = Expression.Call(property, endsWithMethod, constant);
                        }
                    }
                    break;

                case "is empty":
                    if (propertyInfo.PropertyType == typeof(string) || property.Type == typeof(string))
                    {
                        filterExpression = Expression.Equal(property, Expression.Constant(string.Empty));
                    }
                    break;

                case "is not empty":
                    if (propertyInfo.PropertyType == typeof(string) || property.Type == typeof(string))
                    {
                        filterExpression = Expression.NotEqual(property, Expression.Constant(string.Empty));
                    }
                    break;
            }

            if (filterExpression is not null)
            {
                var lambda = Expression.Lambda<Func<T, bool>>(filterExpression, parameter);
                specificationBuilder.Where(lambda);
            }
        }

        return specificationBuilder;
    }

    public static ISpecificationBuilder<T> ApplySorting<T>(this ISpecificationBuilder<T> specificationBuilder, IEnumerable<Sort> sorts)
    {
        if (specificationBuilder is null)
        {
            throw new ArgumentNullException(nameof(specificationBuilder));
        }

        if (sorts is null || !sorts.Any())
        {
            return specificationBuilder;
        }

        bool isFirstSort = true;

        foreach (var sort in sorts)
        {
            if (string.IsNullOrEmpty(sort.SortBy))
            {
                continue;
            }

            var propertyInfo = typeof(T).GetProperty(sort.SortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo is null)
            {
                continue;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyInfo);


            // Handle value objects (e.g., Email.Value)
            if (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType != typeof(string))
            {
                var underlyingPropertyInfo = propertyInfo.PropertyType.GetProperty("Value", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (underlyingPropertyInfo is not null)
                {
                    property = Expression.Property(property, underlyingPropertyInfo);
                }
            }

            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            if (lambda is null)
            {
                continue;
            }

            if (isFirstSort)
            {
                if (sort.Descending)
                {
                    specificationBuilder.OrderByDescending(lambda!);
                }
                else
                {
                    specificationBuilder.OrderBy(lambda!);
                }
                isFirstSort = false;
            }
            else
            {
                if (sort.Descending)
                {
                    specificationBuilder.OrderByDescending(lambda!);
                }
                else
                {
                    specificationBuilder.OrderBy(lambda!);
                }
            }
        }

        return specificationBuilder;
    }

    public static ISpecificationBuilder<T> ApplyPaging<T>(this ISpecificationBuilder<T> specificationBuilder, int? pageNumber, int? pageSize)
    {
        if (pageNumber.HasValue)
        {
            specificationBuilder.Skip((pageNumber.Value - 1) * (pageSize ?? 1000));
        }
        if (pageSize.HasValue)
        {
            specificationBuilder.Take(pageSize.Value);
        }

        return specificationBuilder;
    }
}
