using Asp.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ChristopherBriddock.AspNetCore.Extensions;

/// <summary>
/// A collection of extension methods that enhance the functionality of the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Swagger with custom configuration.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <returns>The modified IServiceCollection instance.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFile)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSwaggerGen(opt =>
        { 
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            opt.IncludeXmlComments(xmlPath);

            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
        });

        return services;
    }

    /// <summary>
    /// Adds API version support.
    /// </summary>
    /// <param name="services">The IServiceCollection instance.</param>
    /// <param name="majorVersion">The major version number.</param>
    /// <param name="minorVersion">The minor version number.</param>
    /// <returns>The modified IServiceCollection instance.</returns>
    public static IServiceCollection AddVersioning(this IServiceCollection services,
                                                   int majorVersion,
                                                   int minorVersion)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(services, nameof(majorVersion));
        ArgumentNullException.ThrowIfNull(services, nameof(minorVersion));

        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });

        return services;
    }
}