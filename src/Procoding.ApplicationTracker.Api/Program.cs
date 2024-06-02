using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Procoding.ApplicationTracker.Api.Extensions;
using Procoding.ApplicationTracker.Api.Infrastructure;
using Procoding.ApplicationTracker.Api.Validation;
using Procoding.ApplicationTracker.Application.Authentication;
using Procoding.ApplicationTracker.Application.Authentication.JwtTokens;
using Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Application.JobApplicationSources.Query.GetJobApplicationSources;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Infrastructure;
using Procoding.ApplicationTracker.Infrastructure.Authentication;
using Procoding.ApplicationTracker.Infrastructure.Data;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
namespace Procoding.ApplicationTracker.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGenWithBearerAuthorization("Bearer");

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

        builder.Services.AddIdentityCore<Employee>()
                        .AddUserStore<EmployeeUserStore>()
                        .AddEntityFrameworkStores<ApplicationDbContext>();

        var jwtTokenOptions = builder.Configuration.GetSection("EmployeeJwtTokenSettings").Get<JwtTokenOptions<Employee>>()!;

        builder.Services.AddAuthentication(x =>
        {
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(config =>
        {
            SecurityKey? secretKey = new JwtTokenCreator<Employee>(jwtTokenOptions).GetDefaultSigningCredentials(secretkey: jwtTokenOptions!.SecretKey).Key;

            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtTokenOptions.Issuer,
                ValidAudience = jwtTokenOptions.Audience,
                IssuerSigningKey = secretKey,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
        }).AddJwtCreator<Employee>(builder.Configuration, "EmployeeJwtTokenSettings");


        builder.Services.AddAuthorization();

        var app = builder.Build();

        //using ApplicationDbContext context = await SeedDatabaseAsync(app);

        app.MapDefaultEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseExceptionHandler();

        app.UseAuthentication();
        app.UseRouting();
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

