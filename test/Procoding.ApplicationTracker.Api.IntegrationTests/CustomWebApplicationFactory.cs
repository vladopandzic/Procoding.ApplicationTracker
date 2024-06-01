using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Infrastructure;
using Procoding.ApplicationTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Api.Program>
{
    public TestDatabaseHelper TestDatabaseHelper;
    private readonly Action<IServiceCollection> _configureServices;


    public CustomWebApplicationFactory(TestDatabaseHelper testDatabaseHelper, Action<IServiceCollection> configureServices)
    {
        TestDatabaseHelper = testDatabaseHelper;
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

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices((services) =>
        {
            // Remove the existing DbContextOptions
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
#pragma warning disable EF1001 // Internal EF Core API usage.
            var connectionString = TestDatabaseHelper.GetDbContextOptions()!.FindExtension<SqlServerOptionsExtension>()!.ConnectionString;
#pragma warning restore EF1001 // Internal EF Core API usage.
            // Add DbContext using the connection string from TestDatabaseHelper

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

        });
    }
}
