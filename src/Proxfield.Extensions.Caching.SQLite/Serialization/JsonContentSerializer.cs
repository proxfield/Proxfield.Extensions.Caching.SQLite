using System.Text.Json;

namespace Proxfield.Extensions.Caching.SQLite.Serialization
{
    public class JsonContentSerializer
    {
        public static string Serialize<T>(T obj) => 
            JsonSerializer.Serialize(obj);

        public static T? Deserialize<T>(string content) =>
            JsonSerializer.Deserialize<T>(content);
    }
}
