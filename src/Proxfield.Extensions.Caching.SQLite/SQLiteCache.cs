using Proxfield.Extensions.Caching.SQLite.Data;
using Proxfield.Extensions.Caching.SQLite.Extensions;
using Proxfield.Extensions.Caching.SQLite.Sql.Models;
using Proxfield.Extensions.Caching.SQLite.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Proxfield.Extensions.Caching.SQLite
{
    /// <summary>
    /// SQLite Caching 
    /// </summary>
    public class SQLiteCache : IDisposable, ISQLiteCache
    { 
        private readonly SQLiteCacheOptions _options;
        private readonly DbCacheOperations _cacheOperations;
        public Maintenance Maintenance { get; set; }
        public SQLiteCache(Action<SQLiteCacheOptions>? options = null)
        {
            _options = new SQLiteCacheOptions();
            options?.Invoke(_options);
            _options.Location ??= PathUtils.ConvertToCurrentLocation("db.sqlite");

            _cacheOperations = new DbCacheOperations(_options.Location);
            this.Maintenance = new Maintenance(_options);
        }

        public SQLiteCache(SQLiteCacheOptions options, 
            DbCacheOperations cacheOperations,
            Maintenance maintenance)
        {
            _cacheOperations = cacheOperations;
            _options = options;
            Maintenance = maintenance;
        }

        //<inheritdoc />
        public void ClearCache() => _cacheOperations.Truncate();

        //<inheritdoc />
        public byte[] Get(string key)
        {
            return _cacheOperations.GetCache(key);
        }

        //<inheritdoc />
        public List<SQLiteCacheEntity> GetStartsWith(string key, int start = 0, int pageSize = int.MaxValue)
        {
            return _cacheOperations.GetStartsWithCache(key.RemoveSpecialChars(), start, pageSize);
        }
        //<inheritdoc />
        public Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            return Task.FromResult(_cacheOperations.GetCache(key));
        }

        //<inheritdoc />
        public Task<List<SQLiteCacheEntity>> GetStartsWithAsync(string key, int start = 0, int pageSize = int.MaxValue, CancellationToken token = default)
        {
            return Task.FromResult(_cacheOperations.GetStartsWithCache(key, start, pageSize));
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
                key = Maintenance.Generate(key);

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