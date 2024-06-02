using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Procoding.ApplicationTracker.Application.Authentication.JwtTokens;

namespace Procoding.ApplicationTracker.Application.Authentication;

public static class ServiceCollectionExtensions
{


    /// <summary>
    /// Registers <see cref="JwtTokenCreator"/> service with <see cref="JwtTokenOptions"/> defined in <paramref
    /// name="sectionName"/> of appSettings file.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    public static IServiceCollection AddJwtCreator<T>(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        var jwtSettings = new JwtTokenOptions<T>();
        configuration.GetSection(sectionName).Bind(jwtSettings);

        services.AddSingleton(jwtSettings);
        services.AddTransient<IJwtTokenCreator<T>, JwtTokenCreator<T>>();

        return services;
    }

    public static AuthenticationBuilder AddJwtCreator<T>(this AuthenticationBuilder builder, IConfiguration configuration, string sectionName)
    {
        var jwtSettings = new JwtTokenOptions<T>();
        configuration.GetSection(sectionName).Bind(jwtSettings);

        builder.Services.AddSingleton(jwtSettings);
        builder.Services.AddTransient<IJwtTokenCreator<T>, JwtTokenCreator<T>>();

        return builder;
    }
}
