using Procoding.ApplicationTracker.Application;

namespace Procoding.ApplicationTracker.Web.Root;

internal class Program
{
    static async Task Main(string[] args)
    {
        var app = new AppAdapter(args, typeof(Program), x =>
        {
            x.AddTransient<ISomething, Something>();
        });

        await app.StartAsync();
    }
}
