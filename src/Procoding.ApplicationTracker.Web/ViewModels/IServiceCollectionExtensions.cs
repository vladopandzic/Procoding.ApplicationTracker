namespace Procoding.ApplicationTracker.Web.ViewModels;

public static class IServiceCollectionExtensions
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<JobApplicationSourceListViewModel, JobApplicationSourceListViewModel>();
    }
}
