using Proxfield.Extensions.Caching.SQLite.Data;
using Proxfield.Extensions.Caching.SQLite.Extensions;
using Proxfield.Extensions.Caching.SQLite.Sql.Models;
using System.Collections.Generic;
using System.Linq;

namespace Proxfield.Extensions.Caching.SQLite
{
    /// <summary>
    /// Performs database maintenance operations
    /// </summary>
    public class Maintenance
    {
        private readonly SQLiteCacheOptions _options;
        private readonly DbIndexOperations _indexOperations;

        public Maintenance(SQLiteCacheOptions options)
        {
            this._options = options;
            this._indexOperations = new DbIndexOperations(options.Location!);
        }
        /// <summary>
        /// Generates an index in case it doesn't exists, if not increments
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        internal string Generate(string key)
        {
            var keyWithoutSpecialChars = key.RemoveSpecialChars();

            if (!_indexOperations.IndexExists(keyWithoutSpecialChars))
                _indexOperations.CreateIndex(keyWithoutSpecialChars);

            return _indexOperations.Next(keyWithoutSpecialChars);
        }
        /// <summary>
        /// Returns all indexes on the database
        /// </summary>
        /// <returns></returns>
        public virtual List<SQLiteCacheIndex> GetAllIndexes() =>
            _indexOperations.GetAllIndexes();
        /// <summary>
        /// Returns an index from the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual SQLiteCacheIndex? GetIndex(string name) =>
            _indexOperations.GetIndexes(name).FirstOrDefault();
        /// <summary>
        /// Purge all indexes from the database
        /// </summary>
        public virtual void ClearAllIndexers() => _indexOperations.ClearAllIndexers();
        /// <summary>
        /// Reset an index to an specific value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public virtual void ResetIndex(string name, long? value = null) => _indexOperations.ResetIndex(name, value);
    }
}
