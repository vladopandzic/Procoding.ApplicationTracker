using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Procoding.ApplicationTracker.Infrastructure.Data;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Api.Program>
{
    public TestDatabaseHelper TestDatabaseHelper;
    private readonly Action<IServiceCollection> _configureServices;
    public string ConnectionString { get; }

    public CustomWebApplicationFactory(TestDatabaseHelper testDatabaseHelper, string connectionString, Action<IServiceCollection> configureServices)
    {
        TestDatabaseHelper = testDatabaseHelper;
        ConnectionString = connectionString;
        _configureServices = configureServices ?? throw new ArgumentNullException(nameof(configureServices));
    }


    //protected override void ConfigureWebHost(IWebHostBuilder builder)
    //{
    //    builder.ConfigureServices(services =>
    //    {
    //        // Call the base ConfigureWebHost method
    //        base.ConfigureWebHost(builder);

    //        // Configure services
    //        _configureServices(services);
    //    });
    //}
    protected override IHostBuilder? CreateHostBuilder()
    {
        return base.CreateHostBuilder();
    }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices((services) =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.AddDbContext<ApplicationDbContext>((_, option) => option.UseNpgsql(ConnectionString));
        });

        //        builder.ConfigureServices((services) =>
        //        {
        //            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

        //            if (descriptor != null)
        //            {
        //                services.Remove(descriptor);
        //            }
        //#pragma warning disable EF1001 // Internal EF Core API usage.
        //            var connectionString = TestDatabaseHelper.GetDbContextOptions()!.FindExtension<SqlServerOptionsExtension>()!.ConnectionString;
        //#pragma warning restore EF1001 // Internal EF Core API usage.
        //            // Add DbContext using the connection string from TestDatabaseHelper

        //            services.AddDbContext<ApplicationDbContext>(options =>
        //            {
        //                options.UseSqlServer(connectionString);
        //            });
        //        });
    }
}
