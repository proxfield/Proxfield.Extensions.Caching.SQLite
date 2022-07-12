using Microsoft.Extensions.DependencyInjection;

namespace Proxfield.Extensions.Caching.SQLite.DependencyInjection
{
    /// <summary>
    /// SQLite Cache Extension for dependency injection
    /// </summary>
    public static class SQLiteCacheExtension
    {
        /// <summary>
        /// Adds all dependencies for the SQLite Cache to work
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddSQLiteCache(this IServiceCollection services, Action<SQLiteCacheOptions> options)
        {
            services.AddScoped<ISQLiteCache, SQLiteCache>(c => new SQLiteCache(options));
            services.AddScoped<Cacheable>();
            return services;
        }
    }
}