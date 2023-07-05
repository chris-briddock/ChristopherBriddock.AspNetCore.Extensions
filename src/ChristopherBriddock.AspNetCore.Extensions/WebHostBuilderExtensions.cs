using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ChristopherBriddock.AspNetCore.Extensions;

public static class WebHostBuilderExtensions
{
    /// <summary>
    /// Adds Kestrel, and uses HTTP/3. Is backward compatible with HTTP/2 and HTTP/1
    /// </summary>
    /// <param name="webHostBuilder"></param>
    public static void AddKestrelConfiguration(this IWebHostBuilder webHostBuilder)
    {
        webHostBuilder.ConfigureKestrel((context, options) =>
        {
            options.ListenAnyIP(7181, listenOptions =>
            {
                listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
                listenOptions.UseHttps();
            });
        });
    }
}
