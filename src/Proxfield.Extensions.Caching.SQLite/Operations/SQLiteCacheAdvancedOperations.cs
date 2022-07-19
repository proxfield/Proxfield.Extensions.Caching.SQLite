using Proxfield.Extensions.Caching.SQLite.Data;
using Proxfield.Extensions.Caching.SQLite.Extensions;

namespace Proxfield.Extensions.Caching.SQLite.Operations
{
    public class SQLiteCacheAdvancedOperations
    {
        private readonly DbIndexOperations _indexOperations;
        public SQLiteCacheAdvancedOperations(DbOperations helper)
        {
            _indexOperations = new DbIndexOperations(helper.GetLocation());
        }

        public string Generate(string key)
        {
            var keyWithoutSpecialChars =  key.RemoveSpecialChars();

            if (!_indexOperations.IndexExists(keyWithoutSpecialChars))
                _indexOperations.CreateIndex(keyWithoutSpecialChars);

            return _indexOperations.Next(keyWithoutSpecialChars);
        }

        public virtual Dictionary<string, long> GetAllIndexes() => _indexOperations.GetAllIndexes();
        public virtual Dictionary<string, long> GetIndexes(string name) => _indexOperations.GetIndexes(name);
        public virtual void ClearAllIndexers() => _indexOperations.ClearAllIndexers();
        public virtual void ResetIndex(string name, long? value = null) => _indexOperations.ResetIndex(name, value);
    }
}
