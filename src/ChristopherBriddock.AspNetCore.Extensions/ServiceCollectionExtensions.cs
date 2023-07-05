using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text;

namespace ChristopherBriddock.AspNetCore.Extensions;

/// <summary>
/// An abstraction for extension methods that add functionality to the service collection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds swagger with custom configuration.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(opt =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            opt.IncludeXmlComments(xmlPath);
        });
        return services;
    }
    /// <summary>
    /// Adds API version support.
    /// </summary>
    /// <param name="services"></param>
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

}
