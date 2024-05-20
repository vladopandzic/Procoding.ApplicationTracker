using MudBlazor.Services;
using Polly;
using Procoding.ApplicationTracker.Application;
using Procoding.ApplicationTracker.Infrastructure.Data;
using Procoding.ApplicationTracker.Web.Services;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.ViewModels;

namespace Procoding.ApplicationTracker.Web.Root;

internal class Program
{
    static async Task Main(string[] args)
    {
        var app = new AppAdapter(args, typeof(Program), x =>
        {
            x.AddTransient<ISomething, Something>();
            x.AddViewModels();
            x.AddMudServices();
            var baseApiUrl = "https://localhost:7140/";
            x.AddHttpClient<IJobApplicationSourceService, JobApplicationSourceService>(x => x.BaseAddress = new Uri(baseApiUrl))
                      .AddTransientHttpErrorPolicy(policyBuilder =>
                                                                                                policyBuilder.WaitAndRetryAsync(
                                                                                                    3, retryNumber => TimeSpan.FromMilliseconds(600)));
            x.AddHttpClient<ICompanyService, CompanyService>(x => x.BaseAddress = new Uri(baseApiUrl))
                     .AddTransientHttpErrorPolicy(policyBuilder =>
                                                                                               policyBuilder.WaitAndRetryAsync(
                                                                                                   3, retryNumber => TimeSpan.FromMilliseconds(600)));
            x.AddHttpClient<ICandidateService, CandidateService>(x => x.BaseAddress = new Uri(baseApiUrl))
                     .AddTransientHttpErrorPolicy(policyBuilder =>
                                                                                               policyBuilder.WaitAndRetryAsync(
                                                                                                   3, retryNumber => TimeSpan.FromMilliseconds(600)));
        });


        await app.StartAsync();
    }
}
