using Proxfield.Extensions.Caching.SQLite.Sql.Models;
using Proxfield.Extensions.Caching.SQLite.Sql.Schema;

namespace Proxfield.Extensions.Caching.SQLite.Data
{
    public class DbIndexOperations : DbOperations
    {
        public DbIndexOperations(string location) : base(location)
        {
            CreateIfNotExists();
        }

        /// <summary>
        /// Create databatse if it does not exists
        /// </summary>
        public virtual void CreateIfNotExists()
        {
            RunNonQueryCommand(SqlIndexCommands.CREATE_TABLE_COMMAND);
        }

        public virtual List<SQLiteCacheIndex> GetAllIndexes() => GetIndexes();
        public virtual SQLiteCacheIndex? GetIndex(string name) => GetIndexes(name).FirstOrDefault();
        public virtual List<SQLiteCacheIndex> GetIndexes(string? name = null)
        {
            var sql = name == null ? SqlIndexCommands.SELECT_ALL_INDEXES_COMMAND : SqlIndexCommands.SELECT_COMMAND;
            var where = new List<KeyValuePair<string, object>>();
            if(name != null) where.Add(new KeyValuePair<string, object>("name", name));
            return RunQueryCommand<SQLiteCacheIndex>(where, sql);            
        }

        public void ResetIndex(string name, long? index)
        {
            RunNonQueryCommand(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("name", name),
                    new KeyValuePair<string, object>("index", index ?? 0)
                }, SqlIndexCommands.UPDATE_COMMAND);
        }

        public void ClearAllIndexers()
        {
            RunNonQueryCommand(SqlIndexCommands.DELETE_ALL_INDEXES_COMMAND);
        }

        public string Next(string key)
        {
            var kpIndex = GetIndex(key);
            var next = (kpIndex?.Sequence ?? 0)+1;
            UpdateIndex(key, next);
            return $"{key}/{next}";
        }

        private bool UpdateIndex(string name, long index)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("name", name),
                    new KeyValuePair<string, object>("index", index)
                },
               SqlIndexCommands.UPDATE_COMMAND) > 0;
        }

        public bool CreateIndex(string name, int index = 0)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("name", name),
                    new KeyValuePair<string, object>("index", index)
                },
                SqlIndexCommands.INSERT_COMMAND) > 0;
        }

        public bool IndexExists(string name) =>  GetIndexes(name).Any();
    }
}
