using Microsoft.Data.Sqlite;

namespace Proxfield.Extensions.Caching.SQLite.Data
{
    /// <summary>
    /// SQLite Helper
    /// </summary>
    public class DbOperations : IDisposable
    {
        private readonly SqliteConnection _sqliteConnection;
        private readonly string _location;
        public DbOperations(string location)
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
        /// Returns the operations connection
        /// </summary>
        /// <returns></returns>
        public SqliteConnection GetConnection() => this._sqliteConnection;
        /// <summary>
        /// Returns the operations location
        /// </summary>
        /// <returns></returns>
        public string GetLocation() => this._location;
        /// <summary>
        /// Runs a command based on SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int RunNonQueryCommand(string sql)
        {
            return RunNonQueryCommand(new List<KeyValuePair<string, object>>(), sql);
        }
        /// <summary>
        ///  Runs a command based on SQL and KeyParValue
        /// </summary>
        /// <param name="items"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int RunNonQueryCommand(List<KeyValuePair<string, object>> items, string sql)
        {
            var command = _sqliteConnection.CreateCommand();
            command.CommandText = sql;
            items.ForEach(p => command.Parameters.AddWithValue($"${p.Key}", p.Value));
            return command.ExecuteNonQuery();
        }
        /// <summary>
        /// Disposes the connection
        /// </summary>
        public void Dispose()
        {
            _sqliteConnection.Dispose();
        }
    }
}
