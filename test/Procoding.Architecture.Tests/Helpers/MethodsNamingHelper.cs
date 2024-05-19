using System.Reflection;

namespace Procoding.Architecture.Tests.Helpers;

internal class MethodsNamingHelper
{
    public static IEnumerable<MethodInfo> GetAsyncMethodsThatDontEndWith(Type type, string word)
    {
        var methods = type.GetMethods();

        var incorrectlyNamedMethods = methods.Where(method =>
        {
            if (typeof(Task).IsAssignableFrom(method.ReturnType))
            {
                if (method.ReturnType.IsGenericType)
                {
                    return !method.Name.EndsWith(word);
                }
                else
                {
                    return !method.Name.EndsWith(word);
                }
            }
            else
            {
                return false;
            }
        });
        return incorrectlyNamedMethods;
    }

    public static IEnumerable<MethodInfo> GetAsyncMethodsThatDontAcceptCancellationToken(Type type)
    {
        var methods = type.GetMethods();

        var methodsWithoutCancellationToken = methods.Where(method =>
        {
            if (IsAsync(method))
            {
                return !method.GetParameters().Any(param => param.ParameterType == typeof(CancellationToken));
            }
            return false;
        });

        return methodsWithoutCancellationToken;
    }

    public static bool IsAsync(MethodInfo method)
    {
        var returnType = method.ReturnType;

        return typeof(Task).IsAssignableFrom(returnType) ||
               (returnType.IsGenericType && returnType.GetGenericTypeDefinition() == typeof(Task<>));
    }
}
