using Microsoft.OpenApi.Models;

namespace Procoding.ApplicationTracker.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static T GetRequiredScopedService<T>(this IServiceProvider services) where T : notnull
    {
        var scope = services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }

    public static IServiceCollection AddSwaggerGenWithBearerAuthorization(this IServiceCollection services, string name)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition(name,
                                    new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme must be provided. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference =
                        new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = name
                        }
                    },
                    new List<string>()
                }
                });
        });

        return services;
    }
}
