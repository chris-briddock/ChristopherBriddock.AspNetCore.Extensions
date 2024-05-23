using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ChristopherBriddock.AspNetCore.Extensions;

public static class WebHostBuilderExtensions
{
    /// <summary>
    /// Adds Kestrel server configuration to the web host builder.
    /// </summary>
    /// <param name="webHostBuilder">The IWebHostBuilder instance.</param>
    public static void AddKestrelConfiguration(this IWebHostBuilder webHostBuilder, int port)
    {
        webHostBuilder.ConfigureKestrel((context, options) =>
        {
            options.ListenAnyIP(port, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                listenOptions.UseHttps();
            });
        });
    }
}
