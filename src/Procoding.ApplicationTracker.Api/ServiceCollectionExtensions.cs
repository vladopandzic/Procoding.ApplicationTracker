namespace Procoding.ApplicationTracker.Api;

public static class ServiceCollectionExtensions
{
    public static T GetRequiredScopedService<T>(this IServiceProvider services) where T : notnull
    {
        var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }
}
