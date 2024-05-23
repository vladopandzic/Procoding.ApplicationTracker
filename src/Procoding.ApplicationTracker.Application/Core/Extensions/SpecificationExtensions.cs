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

            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyExpression = GetNestedPropertyExpression(parameter, filter.Key);
            if (propertyExpression == null)
            {
                continue;
            }
            var constant = Expression.Constant(filter.Value);
            Expression? filterExpression = null;


            switch (filter.Operator?.ToLower())
            {
                case "equals":
                    filterExpression = Expression.Equal(propertyExpression, constant);
                    break;

                case "not equals":
                    filterExpression = Expression.NotEqual(propertyExpression, constant);
                    break;

                case "contains":
                    propertyExpression = GetUnderlyingPropertyExpression(propertyExpression);

                    if (propertyExpression.Type == typeof(string))
                    {
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        if (containsMethod is not null)
                        {
                            filterExpression = Expression.Call(propertyExpression, containsMethod, constant);
                        }
                    }
                    break;

                case "not contains":
                    if (propertyExpression.Type == typeof(string))
                    {
                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        if (containsMethod is not null)
                        {
                            var containsCall = Expression.Call(propertyExpression, containsMethod, constant);
                            filterExpression = Expression.Not(containsCall);
                        }
                    }
                    break;

                case "starts with":
                    if (propertyExpression.Type == typeof(string))
                    {
                        var startsWithMethod = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
                        if (startsWithMethod is not null)
                        {
                            filterExpression = Expression.Call(propertyExpression, startsWithMethod, constant);
                        }
                    }
                    break;

                case "ends with":
                    if (propertyExpression.Type == typeof(string))
                    {
                        var endsWithMethod = typeof(string).GetMethod("EndsWith", new[] { typeof(string) });
                        if (endsWithMethod is not null)
                        {
                            filterExpression = Expression.Call(propertyExpression, endsWithMethod, constant);
                        }
                    }
                    break;

                case "is empty":
                    if (propertyExpression.Type == typeof(string))
                    {
                        filterExpression = Expression.Equal(propertyExpression, Expression.Constant(string.Empty));
                    }
                    break;

                case "is not empty":
                    if (propertyExpression.Type == typeof(string))
                    {
                        filterExpression = Expression.NotEqual(propertyExpression, Expression.Constant(string.Empty));
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

    private static Expression? GetNestedPropertyExpression(Expression parameter, string propertyPath)
    {
        Expression propertyExpression = parameter;
        foreach (var propertyName in propertyPath.Split('.'))
        {
            var propertyInfo = propertyExpression.Type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo == null)
            {
                return null;
            }
            propertyExpression = Expression.Property(propertyExpression, propertyInfo);
        }
        return propertyExpression;
    }

    private static Expression GetUnderlyingPropertyExpression(Expression propertyExpression)
    {
        if (propertyExpression.Type.IsClass && propertyExpression.Type != typeof(string))
        {
            var underlyingPropertyInfo = propertyExpression.Type.GetProperty("Value", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (underlyingPropertyInfo != null)
            {
                propertyExpression = Expression.Property(propertyExpression, underlyingPropertyInfo);
            }
        }
        return propertyExpression;
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


            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyExpression = GetNestedPropertyExpression(parameter, sort.SortBy);
            if (propertyExpression == null)
            {
                continue;
            }

            propertyExpression = GetUnderlyingPropertyExpression(propertyExpression);
            var converted = Expression.Convert(propertyExpression, typeof(object));
            var lambda = Expression.Lambda<Func<T, object>>(converted, parameter);

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
