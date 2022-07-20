using Proxfield.Extensions.Caching.SQLite.Serialization;
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
            cache.Set(key, BynaryContentSerializer.StringToBytes(value));
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
            cache.Set(key, BynaryContentSerializer.BytesFromObject<T>(value));
        }
        /// <summary>
        /// Set a new cache as object, name will be the same as the class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="value"></param>
        public static void SetAsObject<T>(this ISQLiteCache cache, T value)
        {
            cache.Set(nameof(T), BynaryContentSerializer.BytesFromObject(value));
        }
        /// <summary>
        ///  Get a new cache as string
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAsString(this ISQLiteCache cache, string key)
        {
            return BynaryContentSerializer.BytesToString(cache.Get(key));
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
            return BynaryContentSerializer.ObjectFromBytes<T>(cache.Get(key));
        }
        /// <summary>
        /// Get a list of objects when the key starts with something
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T>? GetAsObjectStartsWith<T>(this ISQLiteCache cache, string key) where T : Cacheable
        {
           return cache.GetStartsWith(key)
                .Select(p =>
                {
                    var obj = BynaryContentSerializer.ObjectFromBytes<T>(p.Value);
                    if (obj == null) obj = default;
                    obj!.Key = p.Key;
                    return obj;
                })
                .ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<string>? GetAsStringStartsWith(this ISQLiteCache cache, string key)
        {
            return cache.GetStartsWith(key)
                .Select(p => BynaryContentSerializer.BytesToString(p.Value))
                .ToList();
        }
    }
}
