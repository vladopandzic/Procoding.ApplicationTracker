using Microsoft.Extensions.FileProviders;
using System.Reflection;

namespace Procoding.ApplicationTracker.Web;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseStaticFilesFromAssembly(this IApplicationBuilder applicationBuilder, Assembly assembly)
    {
        var location = assembly.Location;

        var directoryName = Path.GetDirectoryName(location);

        var physicalFileProvider = new PhysicalFileProvider(Path.Combine(directoryName!, "wwwroot"));

        //Call to this resolves files in wwwroot folder
        applicationBuilder.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = physicalFileProvider
        });

        //Call to this resolves blazor scoped css
        applicationBuilder.UseStaticFiles();

        return applicationBuilder;
    }
}
