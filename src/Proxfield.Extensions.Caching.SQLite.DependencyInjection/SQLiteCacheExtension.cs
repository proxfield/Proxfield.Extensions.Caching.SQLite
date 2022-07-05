using Microsoft.Extensions.DependencyInjection;

namespace Proxfield.Extensions.Caching.SQLite.DependencyInjection
{
    public static class SQLiteCacheExtension
    {
        public static IServiceCollection AddSQLiteCache(this IServiceCollection services, Action<SQLiteCacheOptions> options)
        {
            services.AddScoped<ISQLiteCache, SQLiteCache>(c => new SQLiteCache(options));
            return services;
        }
    }
}