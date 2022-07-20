using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Proxfield.Extensions.Caching.SQLite.Sample
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        public static void Main(string[] args)
        {
            var cache = new SQLiteCache();
            cache.ClearCache();
            cache.Maintenance.ClearAllIndexers();

            var user = new User(name: "Jose");
            const string key = "user|";

            cache.SetAsObject(key, user);
            cache.SetAsObject(key, user);

            cache.SetAsObject("vehicles|", new { Name = "bycicle" }) ;

            var users = cache.GetAsObjectStartsWith<User>(key);
            Console.WriteLine(JsonSerializer.Serialize(users));

            var indexers = cache.Maintenance.GetAllIndexes();
            Console.WriteLine(JsonSerializer.Serialize(indexers));
        }
    }
}