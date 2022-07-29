using Proxfield.Extensions.Caching.SQLite.Sql.Models;

namespace Proxfield.Extensions.Caching.SQLite
{
    /// <summary>
    /// Sqlite Cache Interface
    /// </summary>
    public interface ISQLiteCache
    {       
        /// <summary>
        /// Get a key from the cache
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Get(string key);
        /// <summary>
        /// Get a key from the cache asynchronous
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<byte[]> GetAsync(string key, CancellationToken token = default(CancellationToken));
        /// <summary>
        /// Set a key/value on the cache
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, byte[] value);
        /// <summary>
        /// Set a key/value on the cache asynchronous
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task SetAsync(string key, byte[] value, CancellationToken token = default(CancellationToken));
        /// <summary>
        /// Remove a key/value from the cache
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        ///  Remove a key/value from the cache asynchronous
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RemoveAsync(string key, CancellationToken token = default(CancellationToken));
        /// <summary>
        /// Get values from the cache when key starts with
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<SQLiteCacheEntity> GetStartsWith(string key, int start = 0, int pageSize = int.MaxValue);
        /// <summary>
        /// Get values from the cache when key starts with asynchronous
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<List<SQLiteCacheEntity>> GetStartsWithAsync(string key, int start = 0, int pageSize = int.MaxValue, CancellationToken token = default);
        /// <summary>
        /// Clear all the cache from the database (not including indexers)
        /// </summary>
        void ClearCache();
    }
}
