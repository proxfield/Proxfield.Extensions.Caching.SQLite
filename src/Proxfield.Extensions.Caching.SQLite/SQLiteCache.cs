using Proxfield.Extensions.Caching.SQLite.Data;
using Proxfield.Extensions.Caching.SQLite.Extensions;
using Proxfield.Extensions.Caching.SQLite.Operations;
using Proxfield.Extensions.Caching.SQLite.Utils;

namespace Proxfield.Extensions.Caching.SQLite
{
    public class SQLiteCache : IDisposable, ISQLiteCache
    { 
        private readonly SQLiteCacheOptions _options;

        private readonly DbCacheOperations _cacheOperations;
        public SQLiteCacheAdvancedOperations Advanced { get; set; }

        public SQLiteCache(Action<SQLiteCacheOptions>? options = null)
        {
            _options = new SQLiteCacheOptions();
            options?.Invoke(_options);
            
            _cacheOperations = new DbCacheOperations(_options.Location ?? PathUtils.ConvertToCurrentLocation("db.sqlite"));
            Advanced = new SQLiteCacheAdvancedOperations(_cacheOperations);
        }

        public SQLiteCache(DbCacheOperations cacheOperations)
        {
            _options = new SQLiteCacheOptions();
            _cacheOperations = cacheOperations;
            _cacheOperations.CreateIfNotExists();
            Advanced = new SQLiteCacheAdvancedOperations(_cacheOperations);
        }

        //<inheritdoc />
        public void ClearCache() => _cacheOperations.Truncate();

        //<inheritdoc />
        public byte[] Get(string key)
        {
            return _cacheOperations.GetCache(key);
        }

        //<inheritdoc />
        public Dictionary<string, byte[]> GetStartsWith(string key)
        {
            var keyWithoutSpecialChar = key.RemoveSpecialChars();
            return _cacheOperations.GetStartsWithCache(keyWithoutSpecialChar);
        }
        //<inheritdoc />
        public Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            return Task.FromResult(_cacheOperations.GetCache(key));
        }

        //<inheritdoc />
        public Task<Dictionary<string, byte[]>> GetStartsWithAsync(string key, CancellationToken token = default)
        {
            return Task.FromResult(_cacheOperations.GetStartsWithCache(key));
        }
        //<inheritdoc />
        public void Remove(string key)
        {
            _ = _cacheOperations.Delete(key);
        }
        //<inheritdoc />
        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            return Task.FromResult(_cacheOperations.Delete(key));
        }
        //<inheritdoc />
        public void Set(string key, byte[] value)
        {
            if (key.EndsWith("|"))
                key = Advanced.Generate(key);

            _ = _cacheOperations.InsertCache(key, value);
        }
        //<inheritdoc />
        public Task SetAsync(string key, byte[] value, CancellationToken token = default)
        {
           return Task.FromResult(_cacheOperations.InsertCache(key, value));
        }
        /// <summary>
        /// Dispose the connection
        /// </summary>
        public void Dispose() => this._cacheOperations.Dispose();

    }
}