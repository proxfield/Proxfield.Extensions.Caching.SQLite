using Proxfield.Extensions.Caching.SQLite.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
            cache.Set(key, BynaryContentSerializer.StringToBytes(value, cache.GetProvider()));
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
            cache.Set(key, BynaryContentSerializer.BytesFromObject<T>(value, cache.GetProvider()));
        }
        /// <summary>
        /// Set a new cache as object, name will be the same as the class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="value"></param>
        public static void SetAsObject<T>(this ISQLiteCache cache, T value)
        {
            cache.Set(nameof(T), BynaryContentSerializer.BytesFromObject(value, cache.GetProvider()));
        }

        /// <summary>
        /// Set a new cache as string asynchronously
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task SetAsStringAsync(this ISQLiteCache cache, string key, string value, CancellationToken token = default)
        {
            await cache.SetAsync(key, BynaryContentSerializer.StringToBytes(value, cache.GetProvider()), token);
        }

        /// <summary>
        /// Set a new cache as object asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task SetAsObjectAsync<T>(this ISQLiteCache cache, string key, T value, CancellationToken token = default)
        {
            await cache.SetAsync(key, BynaryContentSerializer.BytesFromObject<T>(value, cache.GetProvider()), token);
        }

        /// <summary>
        /// Set a new cache as object asynchronously, name will be the same as the class
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="value"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task SetAsObjectAsync<T>(this ISQLiteCache cache, T value, CancellationToken token = default)
        {
            await cache.SetAsync(nameof(T), BynaryContentSerializer.BytesFromObject(value, cache.GetProvider()), token);
        }
        /// <summary>
        ///  Get a new cache as string
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetAsString(this ISQLiteCache cache, string key)
        {
            return BynaryContentSerializer.BytesToString(cache.Get(key), cache.GetProvider());
        }
        /// <summary>
        /// Get a new cache as object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetAsObject<T>(this ISQLiteCache cache, string key)
        {
            return BynaryContentSerializer.ObjectFromBytes<T>(cache.Get(key), cache.GetProvider())!;
        }
        /// <summary>
        /// Get a list of objects when the key starts with something
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static List<T>? GetAsObjectStartsWith<T>(this ISQLiteCache cache, string key, int start = 0, int pageSize = int.MaxValue) where T : Cacheable
        {
           return cache.GetStartsWith(key, start, pageSize)
                .Select(p =>
                {
                    var obj = BynaryContentSerializer.ObjectFromBytes<T>(p?.Value, cache.GetProvider());
                    if (obj == null) obj = default;
                    obj!.Key = p.Key;
                    return obj;
                })
                .ToList();
        }
        /// <summary>
        /// Get a list of strings when the key starts with something
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<string>? GetAsStringStartsWith(this ISQLiteCache cache, string key, int start = 0, int pageSize = int.MaxValue)
        {
            return cache.GetStartsWith(key, start, pageSize)
                .Select(p => BynaryContentSerializer.BytesToString(p?.Value, cache.GetProvider()))
                .ToList();
        }

        /// <summary>
        /// Get a new cache as string asynchronously
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<string> GetAsStringAsync(this ISQLiteCache cache, string key, CancellationToken token = default)
        {
            return BynaryContentSerializer.BytesToString(await cache.GetAsync(key, token), cache.GetProvider());
        }

        /// <summary>
        /// Get a new cache as object asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<T> GetAsObjectAsync<T>(this ISQLiteCache cache, string key, CancellationToken token = default)
        {
            return BynaryContentSerializer.ObjectFromBytes<T>(await cache.GetAsync(key, token), cache.GetProvider())!;
        }

        /// <summary>
        /// Get a list of objects when the key starts with something asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<T>?> GetAsObjectStartsWithAsync<T>(this ISQLiteCache cache, string key, int start = 0, int pageSize = int.MaxValue, CancellationToken token = default) where T : Cacheable
        {
            return (await cache.GetStartsWithAsync(key, start, pageSize, token))
                 .Select(p =>
                 {
                     var obj = BynaryContentSerializer.ObjectFromBytes<T>(p?.Value, cache.GetProvider());
                     if (obj == null) obj = default;
                     obj!.Key = p.Key;
                     return obj;
                 })
                 .ToList();
        }

        /// <summary>
        /// Get a list of strings when the key starts with something asynchronously
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<string>?> GetAsStringStartsWithAsync(this ISQLiteCache cache, string key, int start = 0, int pageSize = int.MaxValue, CancellationToken token = default)
        {
            return (await cache.GetStartsWithAsync(key, start, pageSize, token))
                .Select(p => BynaryContentSerializer.BytesToString(p?.Value, cache.GetProvider()))
                .ToList();
        }
    }
}
