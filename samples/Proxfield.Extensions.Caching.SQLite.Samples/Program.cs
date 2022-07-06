

using Proxfield.Extensions.Caching.SQLite;

var cache = new SQLiteCache(options =>
{
    options.Location = @"c:\cache\database.sqlite";
});

for (int i = 0; i < 1000; i++)
{
    cache.SetAsString($"test_{i}", i.ToString());
}

cache.GetAsString("test1");
cache.SetAsObject<User>("key", new User() { Name = "Jose" });

cache.GetAsString("test1");

Console.WriteLine(value);