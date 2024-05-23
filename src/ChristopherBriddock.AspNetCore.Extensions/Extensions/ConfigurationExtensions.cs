using Microsoft.Extensions.Configuration;

namespace ChristopherBriddock.AspNetCore.Extensions;

/// <summary>
/// Contains extension methods for IConfiguration and IConfigurationBuilder to manage configuration settings.
/// </summary>
public static class ConfigurationExtensions
{
    /// <summary>
    /// Gets the specified connection string or throws an exception if it is not found.
    /// </summary>
    /// <param name="configuration">The configuration instance to extend.</param>
    /// <param name="name">The name of the connection string.</param>
    /// <returns>The connection string.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the connection string is not found or is empty.</exception>
    public static string GetConnectionStringOrThrow(this IConfiguration configuration, string name)
    {
        var connectionString = configuration.GetConnectionString(name);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{name}' not found.");
        }

        return connectionString;
    }

    /// <summary>
    /// Gets the specified configuration value or throws an exception if it is not found.
    /// </summary>
    /// <param name="configuration">The configuration instance to extend.</param>
    /// <param name="name">The name of the configuration value.</param>
    /// <returns>The configuration value.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the configuration value is not found or is empty.</exception>
    public static string GetRequiredValueOrThrow(this IConfiguration configuration, string name)
    {
        var value = configuration[name];
        if (string.IsNullOrEmpty(value))
        {
            throw new InvalidOperationException($"Value '{name}' not found.");
        }

        return value;
    }

    /// <summary>
    /// Adds configuration files for specified modules to the configuration builder.
    /// </summary>
    /// <param name="builder">The configuration builder instance to extend.</param>
    /// <param name="modules">An array of module names for which to add configuration files.</param>
    public static void AddModuleConfiguration(this IConfigurationBuilder builder, string[] modules)
    {
        foreach (string module in modules)
        {
            builder.AddJsonFile($"appsettings.{module}.json", optional: true, reloadOnChange: false);
            builder.AddJsonFile($"appsettings.{module}.development.json", optional: true, reloadOnChange: false);
        }
    }
}