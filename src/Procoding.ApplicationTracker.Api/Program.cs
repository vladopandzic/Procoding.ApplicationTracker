using FluentValidation;
using LanguageExt.Common;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Api.Infrastructure;
using Procoding.ApplicationTracker.Api.Validation;
using Procoding.ApplicationTracker.Application;
using Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetJobApplicationSources;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.DTOs.Response.JobApplicationSources;
using Procoding.ApplicationTracker.Infrastructure;
using Procoding.ApplicationTracker.Infrastructure.Data;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using System.Reflection;
namespace Procoding.ApplicationTracker.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddTransient<IMapper, Mapper>();
        builder.Services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssemblies(typeof(Program).Assembly, typeof(GetJobApplicationSourcesQuery).Assembly)
             .AddHandlerValidations();     
        });
        builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("JobApplicationDatabase")));
        builder.Services.AddPersistance();

        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        builder.Services.AddValidatorsFromAssemblies([typeof(CandidateInsertRequestDTOValidator).Assembly, 
                                                      typeof(UpdateCandidateCommand).Assembly]);


        builder.Services.AddFluentValidationAutoValidation();

        builder.Services.AddIdentity<Employee, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders()
            .AddApiEndpoints();

        //x.AddIdentityCore<Candidate>().AddEntityFrameworkStores<ApplicationDbContext>();

        //builder.Services.AddIdentityApiEndpoints<Employee>().AddApiEndpoints();

        var app = builder.Build();

        //using ApplicationDbContext context = await SeedDatabaseAsync(app);

        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.MapIdentityApi<Employee>();


        app.UseHttpsRedirection();

        app.UseExceptionHandler();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static async Task<ApplicationDbContext> SeedDatabaseAsync(WebApplication app)
    {
        var context = app.Services.GetRequiredScopedService<ApplicationDbContext>();
        await SeedData.SeedAsync(context);
        return context;
    }
}

