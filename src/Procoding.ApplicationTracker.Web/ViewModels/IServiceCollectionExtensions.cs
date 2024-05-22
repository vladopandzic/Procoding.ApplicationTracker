using Procoding.ApplicationTracker.Web.ViewModels.Candidates;
using Procoding.ApplicationTracker.Web.ViewModels.Companies;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplications;
using Procoding.ApplicationTracker.Web.ViewModels.JobApplicationSources;

namespace Procoding.ApplicationTracker.Web.ViewModels;

public static class IServiceCollectionExtensions
{
    public static void AddViewModels(this IServiceCollection services)
    {
        services.AddTransient<JobApplicationSourceListViewModel, JobApplicationSourceListViewModel>();
        services.AddTransient<JobApplicationSourceDetailsViewModel, JobApplicationSourceDetailsViewModel>();

        services.AddTransient<CandidateListViewModel, CandidateListViewModel>();
        services.AddTransient<CandidateDetailsViewModel, CandidateDetailsViewModel>();

        services.AddTransient<CompanyListViewModel, CompanyListViewModel>();
        services.AddTransient<CompanyDetailsViewModel, CompanyDetailsViewModel>();

        services.AddTransient<JobApplicationListViewModel, JobApplicationListViewModel>();
        services.AddTransient<JobApplicationDetailsViewModel, JobApplicationDetailsViewModel>();
    }
}
