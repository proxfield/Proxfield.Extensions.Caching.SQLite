using Proxfield.Extensions.Caching.SQLite.Sql.Models;
using Proxfield.Extensions.Caching.SQLite.Sql.Schema;
using System.Collections.Generic;
using System.Linq;

namespace Proxfield.Extensions.Caching.SQLite.Data
{
    /// <summary>
    /// Database Index Operations
    /// </summary>
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
        /// <summary>
        /// Get all indexes from the database
        /// </summary>
        /// <returns></returns>
        public virtual List<SQLiteCacheIndex> GetAllIndexes() => GetIndexes();
        /// <summary>
        /// Get an index based on its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual SQLiteCacheIndex? GetIndex(string name) => GetIndexes(name).FirstOrDefault();
        /// <summary>
        /// Get an index list based on index name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual List<SQLiteCacheIndex> GetIndexes(string? name = null)
        {
            var sql = name == null ? SqlIndexCommands.SELECT_ALL_INDEXES_COMMAND : SqlIndexCommands.SELECT_COMMAND;
            var where = new List<KeyValuePair<string, object>>();
            if(name != null) where.Add(new KeyValuePair<string, object>("name", name));
            return RunQueryCommand<SQLiteCacheIndex>(where, sql);            
        }
        /// <summary>
        /// Set the index value to the given one
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        public void ResetIndex(string name, long? index)
        {
            RunNonQueryCommand(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("name", name),
                    new KeyValuePair<string, object>("index", index ?? 0)
                }, SqlIndexCommands.UPDATE_COMMAND);
        }
        /// <summary>
        /// Removes all indexes from the database
        /// </summary>
        public void ClearAllIndexers()
        {
            RunNonQueryCommand(SqlIndexCommands.DELETE_ALL_INDEXES_COMMAND);
        }
        /// <summary>
        /// Gets the next index
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string Next(string key)
        {
            var kpIndex = GetIndex(key);
            var next = (kpIndex?.Sequence ?? 0)+1;
            ResetIndex(key, next);
            return $"{key}/{next}";
        }
        /// <summary>
        /// Create a new index
        /// </summary>
        /// <param name="name"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool CreateIndex(string name, int index = 0)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("name", name),
                    new KeyValuePair<string, object>("index", index)
                },
                SqlIndexCommands.INSERT_COMMAND) > 0;
        }
        /// <summary>
        /// Checks if the index exist
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IndexExists(string name) =>  GetIndexes(name).Any();
    }
}
