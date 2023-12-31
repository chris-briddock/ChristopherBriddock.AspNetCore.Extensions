﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace ChristopherBriddock.AspNetCore.Extensions;

public static class WebHostBuilderExtensions
{
    /// <summary>
    /// Adds Kestrel server configuration to the web host builder.
    /// </summary>
    /// <param name="webHostBuilder">The IWebHostBuilder instance.</param>
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
