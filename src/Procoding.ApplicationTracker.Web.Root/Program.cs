using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using Polly;
using Procoding.ApplicationTracker.Application;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Infrastructure.Data;
using Procoding.ApplicationTracker.Web.Services;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.ViewModels;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;
using System;

namespace Procoding.ApplicationTracker.Web.Root;

internal class Program
{
    static async Task Main(string[] args)
    {
        var app = new AppAdapter(args, typeof(Program), x =>
        {
            x.AddTransient<ISomething, Something>();
            x.AddViewModels();
            x.AddMudServices(x =>
            {
                x.SnackbarConfiguration.PreventDuplicates = false;
                x.SnackbarConfiguration.MaxDisplayedSnackbars = 20;
                x.SnackbarConfiguration.HideTransitionDuration = 100;
                x.SnackbarConfiguration.ShowTransitionDuration = 200;
            });
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

            x.AddHttpClient<IJobApplicationService, JobApplicationService>(x => x.BaseAddress = new Uri(baseApiUrl))
                   .AddTransientHttpErrorPolicy(policyBuilder =>
                                                                                             policyBuilder.WaitAndRetryAsync(
                                                                                                 3, retryNumber => TimeSpan.FromMilliseconds(600)));

            x.AddTransient<INotificationService, NotificationService>();

            //x.AddIdentity<Employee, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>();
            //x.AddIdentityCore<Candidate>().AddEntityFrameworkStores<ApplicationDbContext>();
        });


        await app.StartAsync();
    }
}
