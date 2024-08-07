using Asp.Versioning;
using ChristopherBriddock.AspNetCore.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace ChristopherBriddock.AspNetCore.Extensions;

/// <summary>
/// A collection of extension methods that enhance the functionality of the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
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
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });

        return services;
    }


    /// <summary>
    /// Extension method for adding authentication services to the IServiceCollection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddJsonWebTokenAuthentication(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.TryAddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerConfigurationOptions>();

        services.ConfigureOptions<JwtBearerOptions>();

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

        return services;
    }
}