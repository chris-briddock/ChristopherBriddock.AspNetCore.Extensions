using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace ChristopherBriddock.AspNetCore.Extensions;

/// <summary>
/// Provides extension methods for configuring OpenTelemetry in an <see cref="IHostApplicationBuilder"/>.
/// </summary>
public static class HostApplicationBuilderExtensions
{
    /// <summary>
    /// Configures OpenTelemetry for the application, setting up tracing, metrics, and logging.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="IHostApplicationBuilder"/>.</returns>
    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder, string serviceName)
    {

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
        });

        builder.Services.AddOpenTelemetry()
                .ConfigureResource(resource =>
                {
                    resource.AddService(serviceName);
                })
                .WithTracing(options =>
                {
                    options.SetSampler<AlwaysOnSampler>();
                    options.AddAspNetCoreInstrumentation();
                    options.AddHttpClientInstrumentation();
                    options.AddSqlClientInstrumentation(o => o.SetDbStatementForText = true);
                    options.AddSource("MassTransit");
                })
                .WithMetrics(options =>
                {
                    options.AddAspNetCoreInstrumentation();
                    options.AddHttpClientInstrumentation();
                    options.AddRuntimeInstrumentation();
                });

        builder.AddOpenTelemetryExporters();
        return builder;
    }

    /// <summary>
    /// Adds OpenTelemetry exporters based on configuration settings.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to configure.</param>
    /// <returns>The configured <see cref="IHostApplicationBuilder"/>.</returns>
    private static IHostApplicationBuilder AddOpenTelemetryExporters(this IHostApplicationBuilder builder)
    {
        var services = builder.Services;

        services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter());
        services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
        services.ConfigureOpenTelemetryLoggerProvider(logger => logger.AddOtlpExporter());

        return builder;
    }
}
