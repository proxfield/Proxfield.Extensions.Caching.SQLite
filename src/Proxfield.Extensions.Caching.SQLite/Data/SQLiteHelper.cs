using Microsoft.Data.Sqlite;
using Proxfield.Extensions.Caching.SQLite.Constants;

namespace Proxfield.Extensions.Caching.SQLite.Data
{
    /// <summary>
    /// SQLite Helper
    /// </summary>
    public class SQLiteHelper : IDisposable
    {
        private readonly SqliteConnection _sqliteConnection;
        private readonly string _location;
        public SQLiteHelper(string location)
        {
            _location = location;
            _sqliteConnection = new SqliteConnection($"Data Source={_location};");
            SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
            this.Initialize();
        }
        /// <summary>
        /// Initializes the database 
        /// </summary>
        public virtual void Initialize()
        {
            if (_sqliteConnection.State != System.Data.ConnectionState.Open)
            {
                _sqliteConnection.Open();
            }
        }
        /// <summary>
        /// Get an specific data from the database based on its key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual byte[] Get(string id)
        {
            var command = _sqliteConnection.CreateCommand();
            command.CommandText = SQLCommands.SELECT_COMMAND;
            command.Parameters.AddWithValue("$id", id);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    return (byte[])reader["VALUE"];
                }
            }

            return Array.Empty<byte>();
        }
        /// <summary>
        /// Create databatse if it does not exists
        /// </summary>
        public virtual void CreateIfNotExists()
        {
            RunNonQueryCommand(SQLCommands.CREATE_TABLE_COMMAND);
        }
        /// <summary>
        /// Inserts a data on the database
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool Insert(string id, byte[] value)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>()
                {
                    new KeyValuePair<string, object>("id", id),
                    new KeyValuePair<string, object>("value", value)
                },
            KeyExists(id) ? SQLCommands.UPDATE_COMMAND : SQLCommands.INSERT_COMMAND) > 0;
        }
        /// <summary>
        /// Check if a specific key exists
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual bool KeyExists(string key) => Get(key) != Array.Empty<byte>();
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
            }, SQLCommands.DELETE_COMMAND) > 0;
        }

        private int RunNonQueryCommand(string sql)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>(), sql);
        }

        private int RunNonQueryCommand(List<KeyValuePair<string, object>> items, string sql)
        {
            var command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            items.ForEach(p => command.Parameters.AddWithValue($"${p.Key}", p.Value));
            return command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            _sqliteConnection.Dispose();
        }
    }
}
