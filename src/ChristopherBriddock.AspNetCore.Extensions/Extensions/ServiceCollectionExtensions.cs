using Asp.Versioning;
using ChristopherBriddock.AspNetCore.Extensions.Constants;
using ChristopherBriddock.AspNetCore.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using Microsoft.OpenApi.Models;
using Serilog;

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
    /// <param name="xmlFile"></param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, string xmlFile)
    {
        services.AddSwaggerGen(opt =>
        {
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            opt.IncludeXmlComments(xmlPath);
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text input below.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
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

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

        return services;
    }

     /// <summary>
    /// Add cross origin policy.
    /// </summary>
    /// <remarks>
    /// This is only enabled in development, by the middleware <see cref="CorsMiddlewareExtensions.UseCors(IApplicationBuilder)"/>
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddCrossOriginPolicy(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddPolicy(CorsConstants.PolicyName, opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyHeader();
                opt.AllowAnyMethod();
            });
        });

        return services;
    }

    /// <summary>
    /// Adds serilog to console by default, optionally adds an external logging server by setting the feature flag.
    /// </summary>
    /// <remarks>
    /// Remeber to add the configuration to your appsettings.json file.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/> to which services will be added.</param>
    /// <returns>The modified <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddSerilogWithConfiguration(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider()
                                   .GetService<IConfiguration>()!;

        var featureManager = services.BuildServiceProvider()
                             .GetService<IFeatureManager>()!;

        services.AddSerilog();

        if (featureManager.IsEnabledAsync(FeatureFlagConstants.Logging).Result)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom
                                                  .Configuration(configuration!)
                                                  .CreateLogger();
        }
        else
        {
            Log.Logger = new LoggerConfiguration().WriteTo
                                                  .Console()
                                                  .CreateLogger();
        }

        services.AddLogging(loggingbuilder =>
        {
            loggingbuilder.AddSerilog(Log.Logger);
        });

        return services;
    }
}