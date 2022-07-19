using Proxfield.Extensions.Caching.SQLite.Data;
using Proxfield.Extensions.Caching.SQLite.Extensions;
using Proxfield.Extensions.Caching.SQLite.Sql.Models;

namespace Proxfield.Extensions.Caching.SQLite
{
    public class Maintenance
    {
        private readonly SQLiteCacheOptions _options;
        private readonly DbIndexOperations _indexOperations;

        public Maintenance(SQLiteCacheOptions options)
        {
            this._options = options;
            this._indexOperations = new DbIndexOperations(options.Location!);
        }

        internal string Generate(string key)
        {
            var keyWithoutSpecialChars = key.RemoveSpecialChars();

            if (!_indexOperations.IndexExists(keyWithoutSpecialChars))
                _indexOperations.CreateIndex(keyWithoutSpecialChars);

            return _indexOperations.Next(keyWithoutSpecialChars);
        }

        public virtual List<SQLiteCacheIndex> GetAllIndexes() =>
            _indexOperations.GetAllIndexes();
        public virtual List<SQLiteCacheIndex> GetIndexes(string name) =>
            _indexOperations.GetIndexes(name);
        public virtual void ClearAllIndexers() => _indexOperations.ClearAllIndexers();
        public virtual void ResetIndex(string name, long? value = null) => _indexOperations.ResetIndex(name, value);
    }
}
