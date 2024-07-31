# Extension Methods for ASP.NET Core

This README provides an overview of extension methods that enhance the functionality of ASP.NET Core services. These methods are designed to make common tasks simpler and more convenient when working with ASP.NET Core applications.

ApplicationBuilderExtensions

### UseDatabaseMigrations Extension Method

This method applies pending migrations for the specified DbContext to the database, creating the database if it does not already exist.

#### Usage

    app.UseDatabaseMigrations<MyDbContext>();

#### Parameters

* **app** (Type: `IApplicationBuilder`): The `IApplicationBuilder` instance.
* **TDbContext** (Type: `DbContext`): The type of the DbContext to use for the migration.

#### Description

The `UseDatabaseMigrations` method applies any pending migrations for the specified DbContext to the database. This ensures that the database schema is up to date with the current model.

### UseDatabaseMigrationsAsync Extension Method

This method asynchronously applies pending migrations for the specified DbContext to the database, creating the database if it does not already exist.

#### Usage

    await app.UseDatabaseMigrationsAsync<MyDbContext>();

#### Parameters

*   **app** (Type: `IApplicationBuilder`): The `IApplicationBuilder` instance.
*   **TDbContext** (Type: `DbContext`): The type of the DbContext to use for the migration.

#### Description

The `UseDatabaseMigrationsAsync` method asynchronously applies any pending migrations for the specified DbContext to the database. This ensures that the database schema is up to date with the current model.

HostApplicationBuilderExtensions
--------------------------------

### ConfigureOpenTelemetry Extension Method

This method configures OpenTelemetry for the application, setting up tracing, metrics, and logging.

#### Usage

    builder.ConfigureOpenTelemetry();

#### Parameters

* **builder** (Type: `IHostApplicationBuilder`): The `IHostApplicationBuilder` instance.

#### Description

The `ConfigureOpenTelemetry` method sets up OpenTelemetry for the application, including tracing, metrics, and logging. It configures OpenTelemetry to use various instrumentation and exporters, and includes settings for tracing and metrics.

ConfigurationExtensions

### GetConnectionStringOrThrow Extension Method

This method retrieves the specified connection string or throws an exception if it is not found.

#### Usage

    var connectionString = configuration.GetConnectionStringOrThrow("MyConnectionString");

#### Parameters

* **configuration** (Type: `IConfiguration`): The configuration instance.
* **name** (Type: `string`): The name of the connection string.

#### Description

The `GetConnectionStringOrThrow` method retrieves the specified connection string from the configuration. If the connection string is not found or is empty, an exception is thrown.

### GetRequiredValueOrThrow Extension Method

This method retrieves the specified configuration value or throws an exception if it is not found.

#### Usage

    var value = configuration.GetRequiredValueOrThrow("MyConfigValue");

#### Parameters

* **configuration** (Type: `IConfiguration`): The configuration instance.
* **name** (Type: `string`): The name of the configuration value.

#### Description

The `GetRequiredValueOrThrow` method retrieves the specified configuration value from the configuration. If the value is not found or is empty, an exception is thrown.

ServiceCollectionExtensions
---------------------------

### AddVersioning Extension Method

This method adds API version support to the `IServiceCollection` instance.

#### Usage

    services.AddVersioning(majorVersion, minorVersion);

#### Parameters

* **services** (Type: `IServiceCollection`): The `IServiceCollection` instance.
* **majorVersion** (Type: `int`): The major version number.
* **minorVersion** (Type: `int`): The minor version number.

#### Description

The `AddVersioning` method configures API versioning for your ASP.NET Core application. It allows you to specify the default API version, assume a default version when not specified, and read the version from the "api-version" header.

### AddJsonWebTokenAuthentication Extension Method

This method adds JWT authentication services to the `IServiceCollection` instance.

#### Usage

    services.AddJsonWebTokenAuthentication();

#### Parameters

*   **services** (Type: `IServiceCollection`): The `IServiceCollection` instance.

#### Description

The `AddJsonWebTokenAuthentication` method configures JWT authentication for your ASP.NET Core application. It sets up the default authentication and challenge schemes to use JWT bearer tokens and registers the necessary services.

WebHostBuilderExtensions

### AddKestrelConfiguration Extension Method

This method adds Kestrel server configuration to the `IWebHostBuilder` instance.

#### Usage

    webHostBuilder.AddKestrelConfiguration(port);

#### Parameters

* **webHostBuilder** (Type: `IWebHostBuilder`): The `IWebHostBuilder` instance.
* **port** (Type: `int`): The port number to listen on.

#### Description

The `AddKestrelConfiguration` method configures Kestrel server settings, specifying that it should listen on any IP address and use HTTP/1.1, HTTP/2, and HTTP/3 protocols with HTTPS