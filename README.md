# Extension Methods for ASP.NET Core

This README provides an overview of extension methods that enhance the functionality of ASP.NET Core services. These methods are designed to make common tasks simpler and more convenient when working with ASP.NET Core applications.

## ServiceCollectionExtensions

### `AddSwagger` Extension Method

This method adds Swagger with custom configuration to the `IServiceCollection` instance.

#### Usage
    Replace MyNamespace.MyProduct with your namespace.
    builder.Services.AddSwagger($"{typeof(Program).Namespace!}.xml");

    Add this line to your .csproj file.
    <GenerateDocumentationFile>True</GenerateDocumentationFile>

#### Parameters

*** `services`** (Type: `IServiceCollection`): The `IServiceCollection` instance.

#### Description

The `AddSwagger` method configures Swagger for API documentation and includes custom settings. It adds XML comments and a security definition for JWT authentication using a Bearer token scheme.

### `AddVersioning` Extension Method

This method adds API version support to the `IServiceCollection` instance.

#### Usage
    
    Program.cs:
    services.AddVersioning(majorVersion, minorVersion);

    YourController.cs
    Add this to the top of your controller class
    [Route("api/v{version:apiVersion}")]

#### Parameters

*   **`services`** (Type: `IServiceCollection`): The `IServiceCollection` instance.
*   **`majorVersion`** (Type: `int`): The major version number.
*   **`minorVersion`** (Type: `int`): The minor version number.

#### Description

The `AddVersioning` method configures API versioning for your ASP.NET Core application. It allows you to specify the default API version, assume a default version when not specified, and read the version from the "api-version" header.

## WebHostBuilderExtensions

### `AddKestrelConfiguration` Extension Method

This method adds Kestrel server configuration to the `IWebHostBuilder` instance.

#### Usage

    webHostBuilder.AddKestrelConfiguration();
    

#### Parameters

*   **`webHostBuilder`** (Type: `IWebHostBuilder`): The `IWebHostBuilder` instance.

#### Description

The `AddKestrelConfiguration` method configures Kestrel server settings, specifying that it should listen on any IP address and use HTTP/1.1, HTTP/2, and HTTP/3 protocols with HTTPS.

