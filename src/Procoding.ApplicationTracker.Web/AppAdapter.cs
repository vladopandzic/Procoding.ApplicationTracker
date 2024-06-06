using Procoding.ApplicationTracker.Web.Components;
using System.Reflection;
using FluentValidation;
using Procoding.ApplicationTracker.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Endpoints.Infrastructure;
using Microsoft.AspNetCore.Components.Web;

namespace Procoding.ApplicationTracker.Web;

public class AppAdapter
{
    private WebApplication _app;

    public AppAdapter(string[] args, Type type, Action<IServiceCollection> options)
    {
        var builder = WebApplication.CreateBuilder(args);

        options.Invoke(builder.Services);

        builder.Services.AddRazorComponents()
                        .AddInteractiveServerComponents(x => x.DetailedErrors = true);

        builder.Services.AddValidatorsFromAssemblyContaining(typeof(Validators.JobApplicationSourceValidator));


        _app = builder.Build();


        if (!_app.Environment.IsDevelopment())
        {
            _app.UseExceptionHandler("/Error");


            _app.UseHsts();
        }

        _app.UseHttpsRedirection();

        _app.UseStaticFilesFromAssembly(Assembly.GetExecutingAssembly());


        var builderRazor = _app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
        _app.UseRouting();
        _app.UseAuthentication(); // Use authentication
        _app.UseAuthorization();


        _app.UseAntiforgery();

        _app.UseEndpoints(x => { });

        _app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");
    }


    public async Task StartAsync()
    {
        await _app.RunAsync();
    }
}
