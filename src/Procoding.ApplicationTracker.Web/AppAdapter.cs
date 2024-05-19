using Procoding.ApplicationTracker.Web.Components;
using System.Reflection;

namespace Procoding.ApplicationTracker.Web;

public class AppAdapter
{
    private WebApplication _app;

    public AppAdapter(string[] args, Type type, Action<IServiceCollection> options)
    {
        var builder = WebApplication.CreateBuilder(args);

        options.Invoke(builder.Services);

        builder.Services.AddRazorComponents()
                        .AddInteractiveServerComponents();

        _app = builder.Build();


        if(!_app.Environment.IsDevelopment())
        {
            _app.UseExceptionHandler("/Error");
            _app.UseHsts();
        }

        _app.UseHttpsRedirection();

        _app.UseStaticFilesFromAssembly(Assembly.GetExecutingAssembly());

        _app.UseAntiforgery();

        _app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
    }

    public async Task StartAsync()
    {
        await _app.RunAsync();
    }
}
