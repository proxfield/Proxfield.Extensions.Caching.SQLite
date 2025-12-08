using Proxfield.Extensions.Caching.SQLite.Security;
using Proxfield.Extensions.Caching.SQLite.Sql.Models;
using Proxfield.Extensions.Caching.SQLite.Sql.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Proxfield.Extensions.Caching.SQLite.Data
{
    /// <summary>
    /// Database cache operations
    /// </summary>
    public class DbCacheOperations : DbOperations
    {
        public DbCacheOperations(string location) : base(location)
        {
            CreateIfNotExists();
        }
        /// <summary>
        /// Clear all the items on the cache database
        /// </summary>
        public void Truncate()
        {
            RunNonQueryCommand(SqlCacheCommands.TRUNCATE_COMMAND);
        }
        /// <summary>
        /// Get an specific data from the database based on its key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual byte[] GetCache(string id)
        {
            return base.RunQueryCommand<SQLiteCacheEntity>(new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("id", id)
            }, SqlCacheCommands.SELECT_CACHE_COMMAND)?
                .FirstOrDefault()?
                .Value ?? Array.Empty<byte>();
        }
        /// <summary>
        /// Get an specific data from the database based on its key (starting with some word)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual List<SQLiteCacheEntity> GetStartsWithCache(string id, int start = 0, int pageSize = int.MaxValue)
        {
            return base.RunQueryLikeCommand<SQLiteCacheEntity>(new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("id", id),
                new KeyValuePair<string, object>("start", start),
                new KeyValuePair<string, object>("pageSize", pageSize)
            }, SqlCacheCommands.SELECT_STARTS_WITH_CACHE_COMMAND);
        }
        /// <summary>
        /// Create databatse if it does not exists
        /// </summary>
        public virtual void CreateIfNotExists() => 
            RunNonQueryCommand(SqlCacheCommands.CREATE_CACHE_TABLE_COMMAND);
        /// <summary>
        /// Inserts a data on the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool InsertCache(string id, byte[] value)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("id", id),
                    new KeyValuePair<string, object>("value", value)
                },
            KeyExists(id) ? SqlCacheCommands.UPDATE_CACHE_COMMAND : SqlCacheCommands.INSERT_CACHE_COMMAND) > 0;
        }
        /// <summary>
        /// Check if a specific key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool KeyExists(string key) => GetCache(key) != Array.Empty<byte>();
        /// <summary>
        /// Delete an item from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(string id)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("id", id)
            }, SqlCacheCommands.DELETE_CACHE_COMMAND) > 0;
        }

        /// <summary>
        /// Get an specific data from the database based on its key asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<byte[]> GetCacheAsync(string id, CancellationToken token = default)
        {
            var result = await base.RunQueryCommandAsync<SQLiteCacheEntity>(new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("id", id)
            }, SqlCacheCommands.SELECT_CACHE_COMMAND, token);

            return result?.FirstOrDefault()?.Value ?? Array.Empty<byte>();
        }

        /// <summary>
        /// Get an specific data from the database based on its key (starting with some word) asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <param name="start"></param>
        /// <param name="pageSize"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual Task<List<SQLiteCacheEntity>> GetStartsWithCacheAsync(string id, int start = 0, int pageSize = int.MaxValue, CancellationToken token = default)
        {
            return base.RunQueryLikeCommandAsync<SQLiteCacheEntity>(new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("id", id),
                new KeyValuePair<string, object>("start", start),
                new KeyValuePair<string, object>("pageSize", pageSize)
            }, SqlCacheCommands.SELECT_STARTS_WITH_CACHE_COMMAND, token);
        }

        /// <summary>
        /// Inserts a data on the database asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<bool> InsertCacheAsync(string id, byte[] value, CancellationToken token = default)
        {
            return await RunNonQueryCommandAsync(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("id", id),
                    new KeyValuePair<string, object>("value", value)
                },
            await KeyExistsAsync(id, token) ? SqlCacheCommands.UPDATE_CACHE_COMMAND : SqlCacheCommands.INSERT_CACHE_COMMAND, token) > 0;
        }

        /// <summary>
        /// Check if a specific key exists asynchronously
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<bool> KeyExistsAsync(string key, CancellationToken token = default) => (await GetCacheAsync(key, token)).Length > 0;

        /// <summary>
        /// Delete an item from the database asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public virtual async Task<bool> DeleteAsync(string id, CancellationToken token = default)
        {
            return await RunNonQueryCommandAsync(new List<KeyValuePair<string, object>>()
            {
                new KeyValuePair<string, object>("id", id)
            }, SqlCacheCommands.DELETE_CACHE_COMMAND, token) > 0;
        }

    }
}
