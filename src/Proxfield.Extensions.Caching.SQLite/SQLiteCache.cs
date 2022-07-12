using Proxfield.Extensions.Caching.SQLite.Data;

namespace Proxfield.Extensions.Caching.SQLite
{
    public class SQLiteCache : IDisposable, ISQLiteCache
    { 
        private readonly SQLiteCacheOptions _options;
        private readonly SQLiteHelper _helper;

        public SQLiteCache(Action<SQLiteCacheOptions> options)
        {
            _options = new SQLiteCacheOptions();
            options?.Invoke(_options);
            _helper = new SQLiteHelper(_options.Location!);
            _helper.CreateIfNotExists();
        }

        public SQLiteCache(SQLiteHelper helper)
        {
            _options = new SQLiteCacheOptions();
            _helper = helper;
            _helper.CreateIfNotExists();
        }

        //<inheritdoc />
        public byte[] Get(string key)
        {
            return _helper.Get(key);
        }
        //<inheritdoc />
        public Task<byte[]> GetAsync(string key, CancellationToken token = default)
        {
            return Task.FromResult(_helper.Get(key));
        }
        //<inheritdoc />
        public void Remove(string key)
        {
            _ = _helper.Delete(key);
        }
        //<inheritdoc />
        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            return Task.FromResult(_helper.Delete(key));
        }
        //<inheritdoc />
        public void Set(string key, byte[] value)
        {
            _ = _helper.Insert(key, value);
        }
        //<inheritdoc />
        public Task SetAsync(string key, byte[] value, CancellationToken token = default)
        {
           return Task.FromResult(_helper.Insert(key, value));
        }
        /// <summary>
        /// Dispose the connection
        /// </summary>
        public void Dispose() => this._helper.Dispose();

    }
}