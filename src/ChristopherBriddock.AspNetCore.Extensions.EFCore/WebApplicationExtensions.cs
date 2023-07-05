using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChristopherBriddock.IdentityServer.Extensions;

/// <summary>
/// An abstraction for WebApplication extension methods.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Migrates the database when the application is started. 
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static async Task UseEfMigrations<TContext>(this WebApplication app) where TContext : DbContext
    {
        AsyncServiceScope scope = app.Services.CreateAsyncScope();
        TContext dbContext = scope.ServiceProvider.GetRequiredService<TContext>();
        await dbContext.Database.MigrateAsync();
    }
}
