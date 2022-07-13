using System.Diagnostics.CodeAnalysis;

namespace Proxfield.Extensions.Caching.SQLite.Sample
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        public static void Main(string[] args)
        {
            var cache = new SQLiteCache();

            const string name = "Jose";
            const string key = "user/1";

            cache.SetAsString(key, name);
            Console.WriteLine(cache.GetAsString(key));
        }
    }
}