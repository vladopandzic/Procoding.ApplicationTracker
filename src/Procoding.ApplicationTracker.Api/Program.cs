using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetJobApplicationSources;
using Procoding.ApplicationTracker.Infrastructure;
using Procoding.ApplicationTracker.Infrastructure.Data;
namespace Procoding.ApplicationTracker.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IMapper, Mapper>();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(GetJobApplicationSourcesQuery).Assembly);
        });
        builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("JobApplicationDatabase")));
        builder.Services.AddPersistance();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

