using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Polly;
using Procoding.ApplicationTracker.Application;
using Procoding.ApplicationTracker.Web.Auth;
using Procoding.ApplicationTracker.Web.Controllers;
using Procoding.ApplicationTracker.Web.Services;
using Procoding.ApplicationTracker.Web.Services.Handlers;
using Procoding.ApplicationTracker.Web.Services.Interfaces;
using Procoding.ApplicationTracker.Web.ViewModels;
using Procoding.ApplicationTracker.Web.ViewModels.Abstractions;

namespace Procoding.ApplicationTracker.Web.Root;

internal class Program
{
    static async Task Main(string[] args)
    {
        var app = new AppAdapter(args,
                                 typeof(Program),
                                 x =>
                                 {
                                     x.AddScoped<TokenProvider>();

                                     x.AddControllersWithViews().AddApplicationPart(typeof(LoginController).Assembly);

                                     x.AddCircuitServicesAccessor();

                                     x.AddScoped<IApplicationAuthenticationService, ApplicationAuthenticationService>();
                                     x.AddScoped<ITokenProvider, LocalStorageTokenProvider>();
                                     x.AddTransient<AuthenticationHandler>();


                                     x.AddScoped<AuthenticationStateProvider, RevalidatingServerAuthenticationState>();
                                     x.AddCascadingAuthenticationState();

                                     x.AddAuthentication().AddCookie(x =>
                                     {
                                         x.LoginPath = "/login";
                                     });

                                     x.AddAuthorization();

                                     x.AddBlazoredLocalStorage();

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
                                     x.AddHttpClient<IJobApplicationSourceService, JobApplicationSourceService>("ServerApi", x => x.BaseAddress = new Uri(baseApiUrl))
                                                 //.AddHttpMessageHandler<AuthenticationHandler>()
                                                 .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)));

                                     x.AddHttpClient<ICompanyService, CompanyService>("ServerApi", x => x.BaseAddress = new Uri(baseApiUrl))
                                                 //.AddHttpMessageHandler<AuthenticationHandler>()

                                                 .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)));
                                     x.AddHttpClient<ICandidateService, CandidateService>("ServerApi", x => x.BaseAddress = new Uri(baseApiUrl))
                                                 //.AddHttpMessageHandler<AuthenticationHandler>()
                                                 .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)));

                                     x.AddHttpClient<IJobApplicationService, JobApplicationService>("ServerApi", x => x.BaseAddress = new Uri(baseApiUrl))
                                                  //.AddHttpMessageHandler<AuthenticationHandler>()
                                                  .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)));

                                     x.AddHttpClient<IEmployeeService, EmployeeService>("ServerApi", x => x.BaseAddress = new Uri(baseApiUrl))
                                                   //.AddHttpMessageHandler<AuthenticationHandler>()
                                                   .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)));

                                     x.AddHttpClient<IAuthService, AuthService>("ServerApi", x => x.BaseAddress = new Uri(baseApiUrl))
                                               .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)));

                                     x.AddTransient<INotificationService, NotificationService>();



                                 });


        await app.StartAsync();
    }
}
