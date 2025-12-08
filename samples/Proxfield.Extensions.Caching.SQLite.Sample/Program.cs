using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Proxfield.Extensions.Caching.SQLite.Sample
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            var cache = new SQLiteCache(options =>
            {
                options.EncryptionKey = "d5644e8105ad77c3c3324ba693e83d8f";
                options.UseEncryption = true;
            });

            await cache.ClearCacheAsync();
            cache.Maintenance.ClearAllIndexers();

            var user = new User(name: "Jose");
            const string key = "user|";

            await cache.SetAsObjectAsync(key, user);
            await cache.SetAsObjectAsync(key, user);

            await cache.SetAsObjectAsync("vehicles|", new { Name = "bycicle" }) ;

            var users = await cache.GetAsObjectStartsWithAsync<User>(key);
            Console.WriteLine(JsonSerializer.Serialize(users));

            var indexers = cache.Maintenance.GetAllIndexes();
            Console.WriteLine(JsonSerializer.Serialize(indexers));

            var limit = await cache.GetStartsWithAsync("user", 0, 1);
            Console.WriteLine(JsonSerializer.Serialize(limit));
        }
    }
}