using System.Text;
using System.Text.Json;

namespace Proxfield.Extensions.Caching.SQLite
{
    public static class SQLiteExtension
    {
        /// <summary>
        /// Set a new cache as string
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetAsString(this ISQLiteCache cache, string key, string value)
        {
            cache.Set(key, Encoding.ASCII.GetBytes(value));
        }
        /// <summary>
        ///  Set a new cache as object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetAsObject<T>(this ISQLiteCache cache, string key, T value)
        {
            cache.Set(key, Encoding.ASCII.GetBytes(JsonSerializer.Serialize(value)));
        }
        /// <summary>
        ///  Get a new cache as string
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAsString(this ISQLiteCache cache, string key)
        {
            return Encoding.UTF8.GetString(cache.Get(key));
        }
        /// <summary>
        /// Get a new cache as object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T? GetAsObject<T>(this ISQLiteCache cache, string key)
        {
            return JsonSerializer.Deserialize<T>(Encoding.UTF8.GetString(cache.Get(key)));
        }
    }
}
