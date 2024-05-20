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
            x.AddHttpClient<IJobApplicationSourceService, JobApplicationSourceService>(x => x.BaseAddress = new Uri("https://localhost:7140/"))
                      .AddTransientHttpErrorPolicy(policyBuilder =>
                                                                                                policyBuilder.WaitAndRetryAsync(
                                                                                                    3, retryNumber => TimeSpan.FromMilliseconds(600)));
        });


        await app.StartAsync();
    }
}
