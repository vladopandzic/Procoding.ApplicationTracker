using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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

        builder.Services.AddIdentityCore<Candidate>()
                        .AddUserStore<CandidateUserStore>()
                        .AddEntityFrameworkStores<ApplicationDbContext>();



        builder.Services.AddAuthentication(x =>
        {
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer("BearerEmployee", config =>
        {
            var jwtTokenOptionsEmployee = builder.Configuration.GetSection("EmployeeJwtTokenSettings").Get<JwtTokenOptions<Employee>>()!;

            SecurityKey? secretKey = new JwtTokenCreator<Employee>(jwtTokenOptionsEmployee).GetDefaultSigningCredentials(secretkey: jwtTokenOptionsEmployee!.SecretKey).Key;

            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtTokenOptionsEmployee.Issuer,
                ValidAudience = jwtTokenOptionsEmployee.Audience,
                IssuerSigningKey = secretKey,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero,

            };
        }).AddJwtCreator<Employee>(builder.Configuration, "EmployeeJwtTokenSettings")
        .AddJwtBearer("BearerCandidate", config =>
        {
            var jwtTokenOptionsCandidate = builder.Configuration.GetSection("CandidateJwtTokenSettings").Get<JwtTokenOptions<Candidate>>()!;
            SecurityKey? secretKey = new JwtTokenCreator<Candidate>(jwtTokenOptionsCandidate).GetDefaultSigningCredentials(secretkey: jwtTokenOptionsCandidate!.SecretKey).Key;

            config.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = (x) =>
                {
                    return Task.CompletedTask;
                }
            };


            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtTokenOptionsCandidate.Issuer,
                ValidAudience = jwtTokenOptionsCandidate.Audience,
                IssuerSigningKey = secretKey,
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero,

            };

        }).AddJwtCreator<Candidate>(builder.Configuration, "CandidateJwtTokenSettings");


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
        var passwordHasher = app.Services.GetRequiredScopedService<IPasswordHasher<Candidate>>();
        await SeedData.SeedAsync(context, passwordHasher);
        return context;
    }
}

