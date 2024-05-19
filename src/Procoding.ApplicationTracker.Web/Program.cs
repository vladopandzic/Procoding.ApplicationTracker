//using Procoding.ApplicationTracker.Application;

using Procoding.ApplicationTracker.Web.ViewModels;

namespace Procoding.ApplicationTracker.Web.Root;

internal class Program
{
    static async Task Main(string[] args)
    {
        var app = new AppAdapter(args, typeof(Program), x =>
        {
            //x.AddTransient<IHandler, Handler>();
        });

        await app.StartAsync();
    }
}
