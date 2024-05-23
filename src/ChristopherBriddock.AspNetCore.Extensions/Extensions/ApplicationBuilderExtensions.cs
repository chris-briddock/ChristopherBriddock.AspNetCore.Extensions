using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ChristopherBriddock.AspNetCore.Extensions;

/// <summary>
/// Contains extension methods for IApplicationBuilder to handle database migrations.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Applies pending migrations for the context to the database. Will create the database if it does not already exist.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext to use for the migration.</typeparam>
    /// <param name="app">The IApplicationBuilder instance to extend.</param>
    /// <returns>The IApplicationBuilder instance.</returns>
    public static IApplicationBuilder UseDatabaseMigrations<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        dbContext.Database.Migrate();
        
        return app;
    }

    /// <summary>
    /// Asynchronously applies pending migrations for the context to the database. Will create the database if it does not already exist.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the DbContext to use for the migration.</typeparam>
    /// <param name="app">The IApplicationBuilder instance to extend.</param>
    /// <returns>A task that represents the completion of the migration operation.</returns>
    public static async Task<IApplicationBuilder> UseDatabaseMigrationsAsync<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
    {
        using var scope = app.ApplicationServices.CreateAsyncScope();
        
        var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
        
        await dbContext.Database.MigrateAsync();
        
        return app;
    }
}
