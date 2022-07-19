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
            cache.Advanced.ClearAllIndexers();

            var user = new User()
            {
                Name = "Jose"
            };
            const string key = "user|";

            cache.SetAsObject(key, user);
            cache.SetAsObject(key, user);

            var users = cache.GetAsObjectStartsWith<User>(key);
            Console.WriteLine(JsonSerializer.Serialize(users));

            var indexers = cache.Advanced.GetAllIndexes();
            Console.WriteLine(JsonSerializer.Serialize(indexers));
        }
    }
}